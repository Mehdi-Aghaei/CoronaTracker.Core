// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace CoronaTracker.Core.Brokers.Queues
{
    public partial class QueueBroker : IQueueBroker
    {
        private readonly IConfiguration configuration;

        public QueueBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeClients();
        }

        private void InitializeClients() =>
            this.CountryQueue = GetQueueClient(nameof(CountryQueue));

        private IQueueClient GetQueueClient(string queueName)
        {
            string queueConnectionString =
                this.configuration.GetConnectionString("ServiceBusConnection");

            return new QueueClient(queueConnectionString, queueName);
        }
    }
}
