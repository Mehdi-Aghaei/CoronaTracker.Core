// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventService
    {
        private static void ValidateExternalCountryEventIsNotNull(ExternalCountryEvent externalCountryEvent)
        {
            if (externalCountryEvent is null)
            {
                throw new NullExternalCountryEventException();
            }
        }
    }
}
