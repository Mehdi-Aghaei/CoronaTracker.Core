// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountries.Exceptions;
using CoronaTracker.Core.Services.Foundations.ExternalCountries;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountries
{
    public partial class ExternalCountryProcessingServiceTests
    {
        private readonly Mock<IExternalCountryService> externalCountryServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IExternalCountryProcessingService externalCountryProcessingService;

        public ExternalCountryProcessingServiceTests()
        {
            this.externalCountryServiceMock = new Mock<IExternalCountryService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.externalCountryProcessingService = new ExternalCountryProcessingService(
                externalCountryService: this.externalCountryServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyExceptions()
        {
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new ExternalCountryDependencyException(innerException),
                new ExternalCountryServiceException(innerException)
            };
        }

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private List<ExternalCountry> CreateRandomExternalCountries() =>
            CreateExternalCountryFiller().Create(count: GetRandomNumber()).ToList();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.Data);
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static Filler<ExternalCountry> CreateExternalCountryFiller()
        {
            var filler = new Filler<ExternalCountry>();

            return filler;
        }
    }
}
