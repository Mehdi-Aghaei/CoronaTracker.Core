using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountries
{
    public interface IExternalCountryService
    {
        ValueTask<List<Country>> RetrieveAllCountriesAsync();
    }
}
