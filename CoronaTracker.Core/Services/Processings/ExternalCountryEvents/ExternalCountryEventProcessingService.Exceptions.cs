// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions;
using CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class ExternalCountryEventProcessingService
    {
        private delegate ValueTask<ExternalCountryEvent> ReturningExternalCountryEventFunction();

        private async ValueTask<ExternalCountryEvent> TryCatch(
            ReturningExternalCountryEventFunction returningExternalCountryEventFunction)
        {
            try
            {
                return await returningExternalCountryEventFunction();
            }
            catch (NullExternalCountryEventProcessingException nullCountryEventProcessingException)
            {
                throw CreateAndLogValidationException(
                    nullCountryEventProcessingException);
            }
            catch (ExternalCountryEventValidationException externalCountryEventValidationException)
            {
                throw CreateAndLogDependencyValidationException(externalCountryEventValidationException);
            }
            catch (ExternalCountryEventDependencyValidationException externalCountryEventDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(externalCountryEventDependencyValidationException);
            }
            catch (ExternalCountryEventDependencyException externalCountryEventDependencyException)
            {
                throw CreateAndLogDependencyException(externalCountryEventDependencyException);
            }
            catch (ExternalCountryEventServiceException externalCountryEventServiceException)
            {
                throw CreateAndLogDependencyException(externalCountryEventServiceException);
            }
            catch (Exception exception)
            {
                var failedExternalCountryEventProcessingServiceException =
                    new FailedExternalCountryEventProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedExternalCountryEventProcessingServiceException);
            }
        }

        private ExternalCountryEventProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var externalCountryEventProcessingValidationException =
                new ExternalCountryEventProcessingValidationException(exception);

            this.loggingBroker.LogError(externalCountryEventProcessingValidationException);

            return externalCountryEventProcessingValidationException;
        }

        private ExternalCountryEventProcessingDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var externalCountryEventProcessingDependencyValidationException =
                new ExternalCountryEventProcessingDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(externalCountryEventProcessingDependencyValidationException);

            return externalCountryEventProcessingDependencyValidationException;
        }

        private ExternalCountryEventProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var externalCountryEventProcessingDependencyException =
                new ExternalCountryEventProcessingDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(externalCountryEventProcessingDependencyException);

            return externalCountryEventProcessingDependencyException;
        }

        private ExternalCountryEventProccesingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var externalcountryEventProccesingServiceException =
                new ExternalCountryEventProccesingServiceException(exception);

            this.loggingBroker.LogError(externalcountryEventProccesingServiceException);

            return externalcountryEventProccesingServiceException;
        }
    }
}
