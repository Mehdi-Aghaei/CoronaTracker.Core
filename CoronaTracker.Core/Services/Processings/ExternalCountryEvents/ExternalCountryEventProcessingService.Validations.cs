// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class ExternalCountryEventProcessingService
    {
        private static void ValidateExternalCountryEventIsNotNull(ExternalCountryEvent externalCountryEvent)
        {
            if (externalCountryEvent is null)
            {
                throw new NullExternalCountryEventProcessingException();
            }
        }
    }
}
