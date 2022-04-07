// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Foundations.Countries.Exceptions;
using CoronaTracker.Core.Services.Foundations.Countries;
using CoronaTracker.Core.Services.Processings.Countries;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData DependencyValidationExceptions()
        {
            string randomMessage = GetrandomString();
            string exceptionMessage = randomMessage;
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new CountryValidationException(innerException),
                new CountryDependencyValidationException(innerException)
            };
        }

        public static TheoryData DependencyExceptions()
        {
            string randomMessage = GetrandomString();
            string exceptionMessage = randomMessage;
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new CountryDependencyException(innerException),
                new CountryServiceException(innerException)
            };
        }

        private static IQueryable<Country> CreateRandomCountries() =>
            CreateCountryFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static IQueryable<Country> CreateRandomCountries(Country country)
        {
            List<Country> randomCountries =
                CreateRandomCountries().ToList();

            randomCountries.Add(country);

            return randomCountries.AsQueryable();
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.Data);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetrandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

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
