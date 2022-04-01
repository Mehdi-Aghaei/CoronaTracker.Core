// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Services.Foundations.ExternalCountryEvents;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class ExternalCountryEventProcessingService : IExternalCountryEventProcessingService
    {
        private readonly IExternalCountryEventService countryEventService;
        private readonly ILoggingBroker loggingBroker;

        public ExternalCountryEventProcessingService(IExternalCountryEventService countryEventService, ILoggingBroker loggingBroker)
        {
            this.countryEventService = countryEventService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ExternalCountryEvent> AddExternalCountryEventAsync(ExternalCountryEvent countryEvent) =>
        TryCatch(async () =>
        {
            ValidateExternalCountryEventIsNotNull(countryEvent);
            return await this.countryEventService.AddExternalCountryEventAsync(countryEvent);
        });
    }
}
