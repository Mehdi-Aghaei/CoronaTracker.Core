// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Services.Foundations.Countries;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public partial class CountryProcessingService : ICountryProcessingService
    {
        private readonly ICountryService countryService;
        private readonly ILoggingBroker loggingBroker;

        public CountryProcessingService(
            ICountryService countryService,
            ILoggingBroker loggingBroker)
        {
            this.countryService = countryService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Country> UpsertCountryAsync(Country country) =>
        TryCatch( async () => 
        {
            ValidateCountry(country);
            Country maybeCountry = RetrieveMatchingCountry(country);

            return maybeCountry switch
            {
                null => await this.countryService.AddCountryAsync(country),
                _ => await this.countryService.ModifyCountryAsync(country)
            };
        });

        private Country RetrieveMatchingCountry(Country country)
        {
            IQueryable<Country> countries =
                this.countryService.RetrieveAllCountries();

            return countries.FirstOrDefault(SameCountryAs(country));
        }

        private static Expression<Func<Country, bool>> SameCountryAs(Country country) =>
            retrievedCountry => retrievedCountry.Id == country.Id;
    }
}
