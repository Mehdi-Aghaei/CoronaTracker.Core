// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Services.Foundations.Countries;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public class CountryProcessingService : ICountryProcessingService
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

        public async ValueTask<Country> UpsertCountryAsync(Country country)
        {
            IQueryable<Country> countries =
                this.countryService.RetrieveAllCountries();
            Country maybeCountry = countries.FirstOrDefault(
                retrievedCountry => retrievedCountry.Id == country.Id);

            if(maybeCountry != null)
            {
                return await this.countryService.ModifyCountryAsync(country);
            }

            return await this.countryService.AddCountryAsync(country);
        }
    }
}
