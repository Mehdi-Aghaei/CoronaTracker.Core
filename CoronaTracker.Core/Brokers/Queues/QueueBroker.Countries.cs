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
        public IQueueClient CountriesQueue { get; set; }

        public async ValueTask EnqueueCountryMessageAsync(Message message) =>
            await this.CountriesQueue.SendAsync(message);

        public void ListenToCountriesQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            MessageHandlerOptions messageHandlerOptions = GetMessageHandlerOptions();

            Func<Message, CancellationToken, Task> messageEventHasndler =
                CompleteCountriesQueueMessageAsync(eventHandler);

            this.CountriesQueue.RegisterMessageHandler(messageEventHasndler, messageHandlerOptions);
        }

        private Func<Message,CancellationToken,Task> CompleteCountriesQueueMessageAsync(
            Func<Message, CancellationToken, Task> handler)
        {
            return async (message, token) =>
            {
                await handler(message, token);
                await this.CountriesQueue.CompleteAsync(message.SystemProperties.LockToken);
            };
        }
    }
}
