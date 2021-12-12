// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Services.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Services.Processings.ExternalCountries
{
    public class ExternalCountryProcessingService : IExternalCountryProcessingService
    {
        private readonly IExternalCountryService externalCountryService;
        private readonly ILoggingBroker loggingBroker;

        public ExternalCountryProcessingService(
            IExternalCountryService externalCountryService,
            ILoggingBroker loggingBroker)
        {
            this.externalCountryService = externalCountryService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<ExternalCountry>> RetrieveAllExternalCountriesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
