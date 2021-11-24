﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountries
{
    public interface IExternalCountryService
    {
        ValueTask<List<ExternalCountry>> RetrieveAllExternalCountriesAsync();
    }
}
