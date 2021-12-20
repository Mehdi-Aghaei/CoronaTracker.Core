// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountries.Exceptions;
using CoronaTracker.Core.Models.Processings.ExternalCountries;
using CoronaTracker.Core.Models.Processings.ExternalCountries.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Processings.ExternalCountries
{
    public partial class ExternalCountryProcessingService
    {
        private delegate ValueTask<List<ExternalCountry>> ReturningExternalCountriesFunction();

        private async ValueTask<List<ExternalCountry>>
        TryCatch(ReturningExternalCountriesFunction returningExternalCountriesFunction)
        {
            try
            {
                return await returningExternalCountriesFunction();

            }
            catch (ExternalCountryDependencyException externalCountryDependencyException)
            {

                throw CreateAndLogDependencyException(externalCountryDependencyException);
            }     
            catch (ExternalCountryServiceException externalCountryServiceException)
            {

                throw CreateAndLogDependencyException(externalCountryServiceException);
            }catch(Exception exception)
            {
                var failedExternalCountryProcessingServiceException =
                    new FailedExternalCountryProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedExternalCountryProcessingServiceException);
            }
        }

        private ExternalCountryProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var externalCountryProcessingServiceException =
                new ExternalCountryProcessingServiceException(exception);

            this.loggingBroker.LogError(externalCountryProcessingServiceException);

            return externalCountryProcessingServiceException;
        }

        private ExternalCountryProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var externalCountryProcessingDependencyException =
               new ExternalCountryProcessingDependencyException(
                   exception.InnerException as Xeption);

            this.loggingBroker.LogError(externalCountryProcessingDependencyException);

            return externalCountryProcessingDependencyException;
        }
    }
}
