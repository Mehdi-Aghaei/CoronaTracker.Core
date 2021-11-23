using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Storages;
using CoronaTracker.Core.Models.Countries;
using Taarafo.Core.Brokers.DateTimes;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public partial class CountryService : ICountryService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public CountryService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Country> AddCountryAsync(Country country) =>
        TryCatch(async () =>
        {
            ValidateCountryOnAdd(country);

            return await this.storageBroker.InsertCountryAsync(country);
        });
    }
}
