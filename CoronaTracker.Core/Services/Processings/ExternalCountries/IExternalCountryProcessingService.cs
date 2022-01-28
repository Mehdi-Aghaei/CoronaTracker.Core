// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;

namespace CoronaTracker.Core.Services.Processings.ExternalCountries
{
    public interface IExternalCountryProcessingService
    {
        ValueTask<List<ExternalCountry>> RetrieveAllExternalCountriesAsync();
    }
}
