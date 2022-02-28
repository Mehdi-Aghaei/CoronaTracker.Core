// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using Microsoft.Azure.ServiceBus;

namespace CoronaTracker.Core.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient CountryQueue { get; set; }

        public async ValueTask EnqueueCountryMessageAsync(Message message) =>
            await this.CountryQueue.SendAsync(message);
    }
}
