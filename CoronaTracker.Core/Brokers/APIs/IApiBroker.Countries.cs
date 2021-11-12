using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;

namespace CoronaTracker.Core.Brokers.APIs
{
    public partial interface IApiBroker
    {
        ValueTask<List<ExternalCountry>> GetAllCountriesAsync();
    }
}
