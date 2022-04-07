// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;

namespace CoronaTracker.Core.Services.Orchestrations.Countries
{
    public interface ICountryOrchestrationService
    {
        ValueTask<IQueryable<Country>> RetrieveAllCountriesAsync();
    }
}
