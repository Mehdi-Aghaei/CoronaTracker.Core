using System;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.DateTimes;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Storages;
using CoronaTracker.Core.Models.Countries;


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

        public IQueryable<Country> RetrieveAllCountries() =>
        TryCatch(() => this.storageBroker.SelectAllCountries());

        public ValueTask<Country> RetrieveCountryByIdAsync(Guid countryId) =>
        TryCatch(async () =>
        {
            ValidateCountryId(countryId);

            Country maybeCountry = await this.storageBroker
               .SelectCountryByIdAsync(countryId);

            ValidateStorageCountry(maybeCountry, countryId);

            return maybeCountry;
        });

        public ValueTask<Country> ModifyCountryAsync(Country country) =>
        TryCatch(async () => 
        {
            ValidateCountryOnModify(country);

            Country maybeCountry = await this.storageBroker
                .SelectCountryByIdAsync(country.Id);

            return await this.storageBroker.UpdateCountryAsync(country);
        });
    }
}
