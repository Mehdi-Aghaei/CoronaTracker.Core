// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public interface ICountryEventProcessingService
    {
        ValueTask<CountryEvent> AddCountryEventAsync(CountryEvent countryEvent);
    }
}
