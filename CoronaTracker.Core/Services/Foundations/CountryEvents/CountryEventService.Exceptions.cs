// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using Microsoft.ServiceBus.Messaging;
using MessagingEntityDisabledException = Microsoft.Azure.ServiceBus.MessagingEntityDisabledException;
using Xeptions;


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
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(unauthorizedAccessException);

                throw CreateAndLogCriticalCountryEventDependencyException(failedCountryEventException);
            }
            catch (MessagingEntityDisabledException messagingEntityDisabledException)
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingEntityDisabledException);

                throw CreateAndLogCriticalCountryEventDependencyException(failedCountryEventException);
            }
            catch (ServerBusyException serverBusyException )
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(serverBusyException);

                throw CreateAndLogCountryEventDependencyException(failedCountryEventException);
            }
            catch (MessagingCommunicationException messagingCommunicationException )
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingCommunicationException);

                throw CreateAndLogCountryEventDependencyException(failedCountryEventException);
            }
            catch (MessagingException messagingException )
            {
                var failedCountryEventException =
                    new FailedCountryEventDependencyException(messagingException);

                throw CreateAndLogCountryEventDependencyException(failedCountryEventException);
            }   
        }

        private CountryEventDependencyException CreateAndLogCriticalCountryEventDependencyException(Xeption exception)
        {
            var countryEventDependencyException =
                new CountryEventDependencyException(exception);

            this.loggingBroker.LogCritical(countryEventDependencyException);

            return countryEventDependencyException;
        }

        private CountryEventDependencyException CreateAndLogCountryEventDependencyException(Xeption exception)
        {
            var countryEventDependencyException = 
                new CountryEventDependencyException(exception);

            this.loggingBroker.LogError(countryEventDependencyException);

            return countryEventDependencyException;
        }
    }
}
