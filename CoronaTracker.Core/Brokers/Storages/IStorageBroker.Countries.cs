using System;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Country> InsertCountryAsync(Country country);
        IQueryable<Country> SelectAllCountries();
        ValueTask<Country> SelectCountryByIdAsync(Guid countryId);
        ValueTask<Country> UpdateCountryAsync(Country country);
    }
}
