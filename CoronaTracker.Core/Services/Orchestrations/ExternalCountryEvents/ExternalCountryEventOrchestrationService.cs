// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Orchestrations.Exceptions;
using CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Xeptions;

namespace CoronaTracker.Core.Services.Orchestrations.ExternalCountryEvents
{
    public partial class ExternalCountryEventOrchestrationService : IExternalCountryEventOrchestrationService
    {
        private readonly IExternalCountryProcessingService externalCountryProcessingService;
        private readonly IExternalCountryEventProcessingService externalCountryEventProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public ExternalCountryEventOrchestrationService(
            IExternalCountryProcessingService externalCountryProcessingService,
            IExternalCountryEventProcessingService externalCountryEventProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.externalCountryProcessingService = externalCountryProcessingService;
            this.externalCountryEventProcessingService = externalCountryEventProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public async void AddExternalCountryToQueueAsync()
        {
            try
            {
                List<ExternalCountry> allExternalCountries =
                await this.externalCountryProcessingService
                    .RetrieveAllExternalCountriesAsync();

                foreach (var externalCountry in allExternalCountries)
                {
                    await AddExternalCountryEventAsync(externalCountry);
                }

            }
            catch (Exception exception)
            {
                var failedExternalCountryOrchestrationServiceException =
                    new FailedExternalCountryOrchestrationServiceException(exception);

                throw CreateAndLogServiceException(failedExternalCountryOrchestrationServiceException);
            }
        }

        private ExternalCountryOrchestrationServiceException CreateAndLogServiceException(Xeption exception)
        {
             var externalCountryOrchestrationServiceException =
                new ExternalCountryOrchestrationServiceException(exception);

            this.loggingBroker.LogError(externalCountryOrchestrationServiceException);

            return externalCountryOrchestrationServiceException;
        }

        private async ValueTask AddExternalCountryEventAsync(ExternalCountry persistedExternalCountry)
        {
            var externalCountryEvent = new ExternalCountryEvent
            {
                ExternalCountry = persistedExternalCountry
            };

            await this.externalCountryEventProcessingService
                .AddExternalCountryEventAsync(externalCountryEvent);
        }
    }
}
