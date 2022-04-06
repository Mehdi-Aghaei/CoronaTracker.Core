// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Orchestrations.Exceptions;
using CoronaTracker.Core.Services.Orchestrations.Countries;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CoronaTracker.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : RESTFulController
    {
        private readonly ICountryOrchestrationService countryOrchestrationService;

        public ValuesController(ICountryOrchestrationService countryOrchestrationService)
        {
            this.countryOrchestrationService = countryOrchestrationService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Country>>> GetAllCountriesAsync()
        {
            try
            {
                var retrievedCounttries = await this.countryOrchestrationService
                    .ProcessAllExternalCountriesAsync();

                return Ok(retrievedCounttries);
            }
            catch (ExternalCountryOrchestrationServiceException externalCountryOrchestrationServiceExceptio)
            {
                return InternalServerError(externalCountryOrchestrationServiceExceptio);
            }
        }
    }
}
