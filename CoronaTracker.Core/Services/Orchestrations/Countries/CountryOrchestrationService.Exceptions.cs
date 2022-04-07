// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Orchestrations.Countries.Exceptions;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Orchestrations.Countries
{
    public partial class CountryOrchestrationService : ICountryOrchestrationService
    {
        private delegate ValueTask<IQueryable<Country>> ReturningCountryFunction();

        private async ValueTask<IQueryable<Country>> TryCatch(
            ReturningCountryFunction returningCountryFunction)
        {
            try
            {
                return await returningCountryFunction();
            }
            catch (CountryProcessingValidationException countryProcessingValidationException)
            {
                throw CreateAndLogDependencyValidationException(countryProcessingValidationException);
            }
            catch (CountryProcessingDependencyValidationException countryProcessingDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(countryProcessingDependencyValidationException);
            }
            catch (CountryProcessingDependencyException countryProcessingDependencyException)
            {
                throw CreateAndLogDependencyException(countryProcessingDependencyException);
            }
            catch (CountryProcessingServiceException countryEventProcessingServiceException)
            {
                throw CreateAndLogDependencyException(countryEventProcessingServiceException);
            }
            catch (Exception serviceException)
            {
                var failedCountryOrchestrationServiceException =
                    new FailedCountryOrchestrationServiceException(serviceException);

                throw CreateAndLogServiceException(failedCountryOrchestrationServiceException);
            }
        }

        private CountryOrchestrationDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var countryOrchestrationDependencyValidationException =
               new CountryOrchestrationDependencyValidationException(
                   exception.InnerException as Xeption);

            this.loggingBroker.LogError(countryOrchestrationDependencyValidationException);

            return countryOrchestrationDependencyValidationException;
        }
        private CountryOrchestrationDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var countryOrchestrationDependencyException =
              new CountryOrchestrationDependencyException(
                  exception.InnerException as Xeption);

            this.loggingBroker.LogError(countryOrchestrationDependencyException);

            return countryOrchestrationDependencyException;
        }

        private CountryOrchestrationServiceException CreateAndLogServiceException(Xeption exception)
        {
            var externalCountryOrchestrationServiceException =
               new CountryOrchestrationServiceException(exception);

            this.loggingBroker.LogError(externalCountryOrchestrationServiceException);

            return externalCountryOrchestrationServiceException;
        }
    }
}
