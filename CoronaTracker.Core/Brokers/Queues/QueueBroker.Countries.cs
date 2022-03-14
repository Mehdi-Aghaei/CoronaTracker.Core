// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CoronaTracker.Core.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient ExternalCountriesQueue { get; set; }

        public async ValueTask EnqueueCountryMessageAsync(Message message) =>
            await this.ExternalCountriesQueue.SendAsync(message);

        public void ListenToExternalCountriesQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            MessageHandlerOptions messageHandlerOptions = GetMessageHandlerOptions();

            Func<Message, CancellationToken, Task> messageEventHasndler =
                CompleteExternalCountriesQueueMessageAsync(eventHandler);

            this.ExternalCountriesQueue.RegisterMessageHandler(messageEventHasndler, messageHandlerOptions);
        }

        private Func<Message,CancellationToken,Task> CompleteExternalCountriesQueueMessageAsync(
            Func<Message, CancellationToken, Task> handler)
        {
            return async (message, token) =>
            {
                await handler(message, token);
                await this.ExternalCountriesQueue.CompleteAsync(message.SystemProperties.LockToken);
            };
        }
        public async ValueTask EnqueueExternalCountryMessageAsync(Message message) =>
            await this.ExternalCountriesQueue.SendAsync(message);
    }
}
