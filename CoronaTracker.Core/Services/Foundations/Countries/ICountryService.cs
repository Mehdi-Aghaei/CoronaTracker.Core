// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public interface ICountryService
    {
        ValueTask<Country> AddCountryAsync(Country country);
        ValueTask<Country> RetrieveCountryByIdAsync(Guid countryId);
        IQueryable<Country> RetrieveAllCountries();
        ValueTask<Country> ModifyCountryAsync(Country country);
    }
}
