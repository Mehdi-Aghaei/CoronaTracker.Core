// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.DateTimes;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using CoronaTracker.Core.Services.Orchestrations.Countries;
using CoronaTracker.Core.Services.Processings.Countries;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Orchestrations.ExternalCountryEvents
{
    public partial class CountryOrchestrationServiceTests
    {
        private readonly Mock<IExternalCountryProcessingService> externalCountryProcessingServiceMock;
        private readonly Mock<ICountryProcessingService> countryProcessinServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICountryOrchestrationService countryOrchestrationService;

        public CountryOrchestrationServiceTests()
        {
            this.externalCountryProcessingServiceMock = new Mock<IExternalCountryProcessingService>();
            this.countryProcessinServiceMock = new Mock<ICountryProcessingService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.countryOrchestrationService = new CountryOrchestrationService(
                externalCountryProcessingService: this.externalCountryProcessingServiceMock.Object,
                countryProcessingService: this.countryProcessinServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        public static TheoryData CountryDependencyValidationExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new CountryProcessingValidationException(someInnerException),
                new CountryProcessingDependencyValidationException(someInnerException)
            };
        }

        public static TheoryData CountryDependencyExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new CountryProcessingDependencyException(someInnerException),
                new CountryProcessingServiceException(someInnerException)
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<ExternalCountry> CreateRandomExternalCountries() =>
            CreateExternalCountryFiller().Create(count: GetRandomNumber()).ToList();

        private static Filler<ExternalCountry> CreateExternalCountryFiller() =>
            new Filler<ExternalCountry>();
    }
}
