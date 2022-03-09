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
    public partial interface IQueueBroker
    {
        ValueTask EnqueueCountryMessageAsync(Message message);
        void ListenToCountriesQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
