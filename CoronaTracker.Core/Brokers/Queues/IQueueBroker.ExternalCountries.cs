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
        ValueTask EnqueueExternalCountryMessageAsync(Message message);
        void ListenToExternalCountriesQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
