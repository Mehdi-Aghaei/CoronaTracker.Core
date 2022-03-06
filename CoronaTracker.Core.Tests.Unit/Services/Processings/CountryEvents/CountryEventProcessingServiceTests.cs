// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using CoronaTracker.Core.Services.Foundations.CountryEvents;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingServiceTests
    {
        private readonly Mock<ICountryEventService> countryEventServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICountryEventProcessingService countryEventProcessingService;

        public CountryEventProcessingServiceTests()
        {
            this.countryEventServiceMock = new Mock<ICountryEventService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.countryEventProcessingService = new CountryEventProcessingService(
                countryEventService: countryEventServiceMock.Object,
                loggingBroker: loggingBrokerMock.Object);

        }
        
        public static TheoryData DependencyExceptions()
        {
            string randomString = GetRandomString();
            string exceptionMessage = randomString;
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new CountryEventDependencyException(innerException),
                new CountryEventServiceException(innerException)
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString(wordCount:GetRandomNumber()).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static CountryEvent CreateRandomCountryEvent() =>
            CreateCountryEventFiller().Create();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Filler<CountryEvent> CreateCountryEventFiller()
        {
            var filler = new Filler<CountryEvent>();

            filler.Setup().OnType<DateTimeOffset>()
                .Use(GetRandomDateTime());
        
            return filler;
        }
    }
}
