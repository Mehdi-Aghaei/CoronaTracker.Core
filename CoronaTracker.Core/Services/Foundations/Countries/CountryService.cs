using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Storages;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public class CountryService : ICountryService
    {
        private readonly IStorageBroker storageBroker;

        private readonly ILoggingBroker loggingBroker;

        public CountryService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Country> AddCountryAsync(Country country) =>
            this.storageBroker.InsertCountryAsync(country);
    }
}
