using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public interface ICountryService
    {
        ValueTask<Country> AddCountryAsync(Country country);
    }
}
