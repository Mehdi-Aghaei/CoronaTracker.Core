// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;

namespace CoronaTracker.Core.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<List<ExternalCountry>> GetAllExternalCountriesAsync();
    }
}
