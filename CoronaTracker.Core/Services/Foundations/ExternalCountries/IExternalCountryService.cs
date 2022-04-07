// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountries
{
    public interface IExternalCountryService
    {
        ValueTask<List<ExternalCountry>> RetrieveAllExternalCountriesAsync();
    }
}
