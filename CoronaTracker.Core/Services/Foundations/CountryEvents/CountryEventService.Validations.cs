// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;

namespace CoronaTracker.Core.Services.Foundations.CountryEvents
{
    public partial class CountryEventService
    {
        private static void ValidateCountryEventIsNotNull(CountryEvent countryEvent)
        {
            if (countryEvent is null)
            {
                throw new NullCountryEventException();
            }
        }
    }
}
