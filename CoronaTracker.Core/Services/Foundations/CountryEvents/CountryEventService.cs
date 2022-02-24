// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.CountryEvents;

namespace CoronaTracker.Core.Services.Foundations.CountryEvents
{
    public class CountryEventService : ICountryEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly ILoggingBroker loggingBroker;

        public CountryEventService(IQueueBroker queueBroker,ILoggingBroker loggingBroker)
        {
            this.queueBroker = queueBroker;
            this.loggingBroker = loggingBroker;
        }
        public ValueTask<CountryEvent> AddCountryEventAsync(CountryEvent countryEvent) =>
            throw new System.NotImplementedException();
    }
}
