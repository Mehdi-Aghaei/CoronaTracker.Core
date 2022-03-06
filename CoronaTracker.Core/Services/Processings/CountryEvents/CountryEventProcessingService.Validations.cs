// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.Processings.CountryEvents;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingService
    {
        private static void ValidateCountryEventIsNotNull(CountryEvent countryEvent)
        {
            if (countryEvent is null)
            {
                throw new NullCountryEventProcessingException();
            }
        }

    }
}
