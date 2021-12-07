// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public partial class CountryProcessingService
    {
        private static void ValidateCountry(Country country)
        {
            if(country is null)
            {
                throw new NullCountryProcessingException();
            }
        }
    }
}
