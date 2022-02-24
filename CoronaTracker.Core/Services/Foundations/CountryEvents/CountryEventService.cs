// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.CountryEvents;
using Microsoft.Azure.ServiceBus;

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
        public async ValueTask<CountryEvent> AddCountryEventAsync(CountryEvent countryEvent)
        {
            Message message = MapToMessage(countryEvent);

            await this.queueBroker.EnqueueCountryMessageAsync(message);

            return countryEvent;
        }

        private static Message MapToMessage(CountryEvent countryEvent)
        {
            string serializedCountryEvent = JsonSerializer.Serialize(countryEvent);

            return new Message
            {
                Body = Encoding.UTF8.GetBytes(serializedCountryEvent)
            };
        }
    }
}
