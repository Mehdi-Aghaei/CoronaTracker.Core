// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Configurations;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.CountryEvents;
using Microsoft.Azure.ServiceBus;

namespace CoronaTracker.Core.Services.Foundations.CountryEvents
{
    public partial class CountryEventService : ICountryEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly IConfigurationBroker configurationBroker;
        private readonly ILoggingBroker loggingBroker;

        public CountryEventService(
            IQueueBroker queueBroker,
            ILoggingBroker loggingBroker,
            IConfigurationBroker configurationBroker)
        {
            this.queueBroker = queueBroker;
            this.configurationBroker = configurationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<CountryEvent> AddCountryEventAsync(CountryEvent countryEvent) =>
        TryCatch(async () =>
        {
            ValidateCountryEventIsNotNull(countryEvent);
            Message message = MapToMessage(countryEvent);

            await this.queueBroker.EnqueueCountryMessageAsync(message);

            return countryEvent;
        });

        private Message MapToMessage(CountryEvent countryEvent)
        {
            countryEvent.TrustedSourceId =
                this.configurationBroker.GetTrustedSourceId();

            string serializedCountryEvent =
                JsonSerializer.Serialize(countryEvent);

            return new Message
            {
                Body = Encoding.UTF8.GetBytes(serializedCountryEvent)
            };
        }
    }
}
