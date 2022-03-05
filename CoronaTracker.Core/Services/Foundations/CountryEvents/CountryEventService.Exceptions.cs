// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using Microsoft.ServiceBus.Messaging;
using Xeptions;
using MessagingEntityDisabledException = Microsoft.Azure.ServiceBus.MessagingEntityDisabledException;


namespace CoronaTracker.Core.Services.Foundations.CountryEvents
{
    public partial class CountryEventService
    {
        private delegate ValueTask<CountryEvent> ReturningCountryEventFunction();

        private async ValueTask<CountryEvent> TryCatch(ReturningCountryEventFunction returningCountryEventFunction)
        {
            try
            {
                return await returningCountryEventFunction();
            }
            catch (NullCountryEventException nullCountryEventException)
            {
                throw CreateAndLogValidationException(nullCountryEventException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(unauthorizedAccessException);

                throw CreateAndLogCriticalDependencyException(failedCountryEventException);
            }
            catch (MessagingEntityDisabledException messagingEntityDisabledException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingEntityDisabledException);

                throw CreateAndLogCriticalDependencyException(failedCountryEventException);
            }
            catch (ServerBusyException serverBusyException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(serverBusyException);

                throw CreateAndLogDependencyException(failedCountryEventException);
            }
            catch (MessagingCommunicationException messagingCommunicationException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingCommunicationException);

                throw CreateAndLogDependencyException(failedCountryEventException);
            }
            catch (MessagingException messagingException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingException);

                throw CreateAndLogDependencyException(failedCountryEventException);
            }
            catch (Exception exception)
            {
                var failedCountryEventServiceException =
                    new FailedCountryEventServiceException(exception);

                throw CreateAndLogServiceException(failedCountryEventServiceException);
            }
        }

        private CountryEventValidationException CreateAndLogValidationException(Xeption exception)
        {
            var countryEventValidationException =
                new CountryEventValidationException(exception);

            this.loggingBroker.LogError(countryEventValidationException);

            return countryEventValidationException;
        }

        private CountryEventDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var countryEventDependencyException =
                new CountryEventDependencyException(exception);

            this.loggingBroker.LogCritical(countryEventDependencyException);

            return countryEventDependencyException;
        }

        private CountryEventDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var countryEventDependencyException =
                new CountryEventDependencyException(exception);

            this.loggingBroker.LogError(countryEventDependencyException);

            return countryEventDependencyException;
        }

        private CountryEventServiceException CreateAndLogServiceException(Xeption exception)
        {
            var countryEventServiceException =
                new CountryEventServiceException(exception);

            this.loggingBroker.LogError(countryEventServiceException);

            return countryEventServiceException;
        }
    }
}
