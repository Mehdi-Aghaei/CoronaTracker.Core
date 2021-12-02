// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Services.Foundations.Countries;
using CoronaTracker.Core.Services.Processings.Countries;
using Moq;
using Tynamix.ObjectFiller;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        private readonly Mock<ICountryService> countryServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICountryProcessingService countryProcessingService;

        public CountryProcessingServiceTests()
        {
            this.countryServiceMock = new Mock<ICountryService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.countryProcessingService = new CountryProcessingService(
                countryService: this.countryServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Country CreateRandomCountry() =>
            CreateCountryFiller().Create();

        private static IQueryable<Country> CreateRandomCountries() =>
            CreateCountryFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static IQueryable<Country> CreateRandomCountries(Country country)
        {
            List<Country> randomCountries =
                CreateRandomCountries().ToList();

            randomCountries.Add(country);

            return randomCountries.AsQueryable();
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Country> CreateCountryFiller()
        {
            var filler = new Filler<Country>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}
