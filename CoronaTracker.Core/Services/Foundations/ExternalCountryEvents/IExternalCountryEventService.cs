// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountryEvents;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public interface IExternalCountryEventService
    {
        ValueTask<ExternalCountryEvent> AddExternalCountryEventAsync(ExternalCountryEvent countryEvent);

        void ListenToExternalCountriesEvent(Func<ExternalCountry, ValueTask> externalCountryEventHandler);
    }
}
