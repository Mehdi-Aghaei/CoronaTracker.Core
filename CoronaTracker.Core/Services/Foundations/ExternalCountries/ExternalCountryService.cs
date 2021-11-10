using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.APIs;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;

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

        public ValueTask<List<ExternalCountry>> RetrieveAllCountriesAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllCountriesAsync());
    }
}
