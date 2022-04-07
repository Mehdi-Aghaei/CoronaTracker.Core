// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions;
using Microsoft.Azure.ServiceBus;
using Xeptions;
using Messaging = Microsoft.ServiceBus.Messaging;


namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventService
    {
        private delegate ValueTask<ExternalCountryEvent> ReturningExternalCountryEventFunction();

        private async ValueTask<ExternalCountryEvent> TryCatch(
            ReturningExternalCountryEventFunction returningExternalCountryEventFunction)
        {
            try
            {
                return await returningExternalCountryEventFunction();
            }
            catch (NullExternalCountryEventException nullCountryEventException)
            {
                throw CreateAndLogValidationException(nullCountryEventException);
            }
            catch (InvalidExternalCountryEventException invalidExternalCountryEventException)
            {
                throw CreateAndLogValidationException(invalidExternalCountryEventException);
            }
            catch (MessagingEntityNotFoundException messagingEntityNotFoundException)
            {
                var failedExternalCountryEventDependencyException =
                    new FailedExternalCountryEventDependencyException(messagingEntityNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryEventDependencyException);
            }
            catch (MessagingEntityDisabledException messagingEntityDisabledException)
            {
                var failedExternalCountryEventException =
                    new FailedExternalCountryEventDependencyException(messagingEntityDisabledException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryEventException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var failedExternalCountryEventException =
                    new FailedExternalCountryEventDependencyException(unauthorizedAccessException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryEventException);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                var failedExternalCountryEventDependencyException =
                    new FailedExternalCountryEventDependencyException(invalidOperationException);

                throw CreateAndLogDependencyException(failedExternalCountryEventDependencyException);
            }
            catch (Messaging.MessagingCommunicationException messagingCommunicationException)
            {
                var failedExternalCountryEventException =
                    new FailedExternalCountryEventDependencyException(messagingCommunicationException);

                throw CreateAndLogDependencyException(failedExternalCountryEventException);
            }
            catch (ServerBusyException serverBusyException)
            {
                var failedExternalCountryEventException =
                    new FailedExternalCountryEventDependencyException(serverBusyException);

                throw CreateAndLogDependencyException(failedExternalCountryEventException);
            }
            catch (ArgumentException argumentException)
            {
                var invalidExternalCountryEventArgumentException =
                    new InvalidExternalCountryEventArgumentException(argumentException);

                throw CreateAndLogDependencyValidationException(invalidExternalCountryEventArgumentException);
            }
            catch (Exception exception)
            {
                var failedExternalCountryEventServiceException =
                    new FailedExternalCountryEventServiceException(exception);

                throw CreateAndLogServiceException(failedExternalCountryEventServiceException);
            }
        }

        private ExternalCountryEventValidationException CreateAndLogValidationException(Xeption exception)
        {
            var externalCountryEventValidationException =
                new ExternalCountryEventValidationException(exception);

            this.loggingBroker.LogError(externalCountryEventValidationException);

            return externalCountryEventValidationException;
        }

        private ExternalCountryEventDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var externalCountryEventDependencyException =
                new ExternalCountryEventDependencyException(exception);

            this.loggingBroker.LogCritical(externalCountryEventDependencyException);

            return externalCountryEventDependencyException;
        }

        private ExternalCountryEventDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var externalcountryEventDependencyException =
                new ExternalCountryEventDependencyException(exception);

            this.loggingBroker.LogError(externalcountryEventDependencyException);

            return externalcountryEventDependencyException;
        }

        private ExternalCountryEventDependencyValidationException CreateAndLogDependencyValidationException(
           Xeption exception)
        {
            var externalCountryEventDependencyValidationException =
                new ExternalCountryEventDependencyValidationException(exception);

            this.loggingBroker.LogError(externalCountryEventDependencyValidationException);

            return externalCountryEventDependencyValidationException;
        }

        private ExternalCountryEventServiceException CreateAndLogServiceException(Xeption exception)
        {
            var externalCountryEventServiceException =
                new ExternalCountryEventServiceException(exception);

            this.loggingBroker.LogError(externalCountryEventServiceException);

            return externalCountryEventServiceException;
        }
    }
}
