// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public interface IExternalCountryEventService
    {
        ValueTask<ExternalCountryEvent> AddExternalCountryEventAsync(ExternalCountryEvent countryEvent);

        void ListenToExternalCountriesEvent(Func<ExternalCountry, ValueTask> externalCountryEventHandler);
    }
}
