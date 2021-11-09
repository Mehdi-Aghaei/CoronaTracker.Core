using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Brokers.APIs
{
    public partial class ApiBroker
    {
        private const string relativeUrl = "v3/covid-19/countries";

        public async ValueTask<List<Country>> GetAllCountriesAsync() =>
            await this.GetAsync<List<Country>>(relativeUrl);
    }
}
