// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Apis;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountries
{
    public partial class ExternalCountryService : IExternalCountryService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public ExternalCountryService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<ExternalCountry>> RetrieveAllExternalCountriesAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllExternalCountriesAsync());
    }
}
