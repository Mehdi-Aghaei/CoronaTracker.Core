using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Brokers.APIs
{
    public partial interface IApiBroker
    {
        ValueTask<List<Country>> GetAllCountriesAsync();
    }
}
