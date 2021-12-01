// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

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

        public ValueTask<Country> UpsertCountryAsync(Country country)
        {
            throw new System.NotImplementedException();
        }
    }
}
