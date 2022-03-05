// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Services.Foundations.CountryEvents;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public class CountryEventProcessingService : ICountryEventProcessingService
    {
        private readonly ICountryEventService countryEventService;
        private readonly ILoggingBroker loggingBroker;

        public CountryEventProcessingService(ICountryEventService countryEventService, ILoggingBroker loggingBroker)
        {
            this.countryEventService = countryEventService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<CountryEvent> AddCountryEventAsync(CountryEvent countryEvent) =>
            await this.countryEventService.AddCountryEventAsync(countryEvent);
    }
}
