// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public interface ICountryProcessingService
    {
        IQueryable<Country> RetrieveAllCountries();
        bool VerifyCountryChanged(Country country);
        ValueTask<Country> UpsertCountryAsync(Country country);
    }
}
