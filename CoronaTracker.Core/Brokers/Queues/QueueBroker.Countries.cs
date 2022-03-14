// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CoronaTracker.Core.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient ExternalCountryQueue { get; set; }

        public async ValueTask EnqueueExternalCountryMessageAsync(Message message) =>
            await this.ExternalCountryQueue.SendAsync(message);
    }
}
