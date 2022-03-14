// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public interface IExternalCountryEventProcessingService
    {
        ValueTask<ExternalCountryEvent> AddExternalCountryEventAsync(ExternalCountryEvent externalCountryEvent);
    }
}
