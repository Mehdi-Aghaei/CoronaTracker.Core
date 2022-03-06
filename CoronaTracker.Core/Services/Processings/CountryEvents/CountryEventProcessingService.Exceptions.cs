// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using Xeptions;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingService
    {
        private delegate ValueTask<CountryEvent> ReturningCountryEventFunction();

        private async ValueTask<CountryEvent> TryCatch(ReturningCountryEventFunction returningCountryEventFunction)
        {
            try
            {
                return await returningCountryEventFunction();
            }
            catch (NullCountryEventProcessingException nullCountryEventProcessingException)
            {
                throw CreateAndLogValidationException(
                    nullCountryEventProcessingException);
            }
            catch (CountryEventDependencyException countryEventDependencyException)
            {
                throw CreateAndLogDependencyException(countryEventDependencyException);
            }
            catch (CountryEventServiceException countryEventServiceException)
            {
                throw CreateAndLogDependencyException(countryEventServiceException);
            }
            catch (Exception exception)
            {
                var failedCountryEventProcessingServiceException =
               new FailedCountryEventProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedCountryEventProcessingServiceException);
            }
        }

        private CountryEventProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var countryEventProcessingValidationException =
                new CountryEventProcessingValidationException(exception);

            this.loggingBroker.LogError(countryEventProcessingValidationException);

            return countryEventProcessingValidationException;
        }

        private CountryEventProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var countryEventProcessingDependencyException =
                new CountryEventProcessingDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(countryEventProcessingDependencyException);

            return countryEventProcessingDependencyException;
        }

        private CountryEventProccesingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var countryEventProccesingServiceException =
                new CountryEventProccesingServiceException(exception);

            this.loggingBroker.LogError(countryEventProccesingServiceException);

            return countryEventProccesingServiceException;
        }
    }
}
