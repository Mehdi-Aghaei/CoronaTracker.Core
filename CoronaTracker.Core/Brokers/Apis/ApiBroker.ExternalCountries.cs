// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string RelativeUrl = "v3/covid-19/countries";

        public async ValueTask<List<ExternalCountry>> GetAllExternalCountriesAsync() =>
            await this.GetAsync<List<ExternalCountry>>(RelativeUrl);
    }
}
