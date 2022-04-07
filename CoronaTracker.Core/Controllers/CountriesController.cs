// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Orchestrations.Countries.Exceptions;
using CoronaTracker.Core.Services.Orchestrations.Countries;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CoronaTracker.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : RESTFulController
    {
        private readonly ICountryOrchestrationService countryOrchestrationService;

        public CountriesController(ICountryOrchestrationService countryOrchestrationService)
        {
            this.countryOrchestrationService = countryOrchestrationService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Country>>> GetAllCountriesAsync()
        {
            try
            {
                IQueryable<Country> retrievedCountries = await this.countryOrchestrationService
                    .RetrieveAllCountriesAsync();

                return Ok(retrievedCountries);
            }
            catch (CountryOrchestrationDependencyValidationException countryOrchestrationDependencyValidationException)
            {
                return BadRequest(countryOrchestrationDependencyValidationException.InnerException);
            }
            catch (CountryOrchestrationDependencyException countryOrchestrationDependencyException)
            {
                return InternalServerError(countryOrchestrationDependencyException);
            }
            catch (CountryOrchestrationServiceException countryOrchestrationServiceException)
            {
                return InternalServerError(countryOrchestrationServiceException);
            }
        }
    }
}
