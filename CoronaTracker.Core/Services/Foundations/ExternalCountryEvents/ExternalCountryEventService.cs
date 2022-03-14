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
using CoronaTracker.Core.Models.ExternalCountryEvents;
using Microsoft.Azure.ServiceBus;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventService : IExternalCountryEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly IConfigurationBroker configurationBroker;
        private readonly ILoggingBroker loggingBroker;

        public ExternalCountryEventService(
            IQueueBroker queueBroker,
            ILoggingBroker loggingBroker,
            IConfigurationBroker configurationBroker)
        {
            this.queueBroker = queueBroker;
            this.configurationBroker = configurationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ExternalCountryEvent> AddExternalCountryEventAsync(ExternalCountryEvent externalCountryEvent) =>
        TryCatch(async () =>
        {
            ValidateExternalCountryEventIsNotNull(externalCountryEvent);
            Message message = MapToMessage(externalCountryEvent);

            await this.queueBroker.EnqueueCountryMessageAsync(message);

            return externalCountryEvent;
        });

        private Message MapToMessage(ExternalCountryEvent externalCountryEvent)
        {
            externalCountryEvent.TrustedSourceId =
                this.configurationBroker.GetTrustedSourceId();

            string serializedCountryEvent =
                JsonSerializer.Serialize(externalCountryEvent);

            return new Message
            {
                Body = Encoding.UTF8.GetBytes(serializedCountryEvent)
            };
        }
    }
}
