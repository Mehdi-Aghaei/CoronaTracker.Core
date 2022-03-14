// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions;
using CoronaTracker.Core.Services.Foundations.ExternalCountryEvents;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountryEvents
{
    public partial class ExternalCountryEventProcessingServiceTests
    {
        private readonly Mock<IExternalCountryEventService> externalCountryEventServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IExternalCountryEventProcessingService externalCountryEventProcessingService;

        public ExternalCountryEventProcessingServiceTests()
        {
            this.externalCountryEventServiceMock = new Mock<IExternalCountryEventService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.externalCountryEventProcessingService = new ExternalCountryEventProcessingService(
                countryEventService: externalCountryEventServiceMock.Object,
                loggingBroker: loggingBrokerMock.Object);

        }

        public static TheoryData DependencyExceptions()
        {
            string randomString = GetRandomString();
            string exceptionMessage = randomString;
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new ExternalCountryEventDependencyException(innerException),
                new ExternalCountryEventServiceException(innerException)
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static ExternalCountryEvent CreateRandomExternalCountryEvent() =>
            CreateExternalCountryEventFiller().Create();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Filler<ExternalCountryEvent> CreateExternalCountryEventFiller()
        {
            var filler = new Filler<ExternalCountryEvent>();

            filler.Setup().OnType<DateTimeOffset>()
                .Use(GetRandomDateTime());

            return filler;
        }
    }
}
