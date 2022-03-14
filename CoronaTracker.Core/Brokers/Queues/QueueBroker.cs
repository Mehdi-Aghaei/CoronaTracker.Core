// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
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
            InitializeQueueClients();
        }

        private void InitializeClients() =>
            this.ExternalCountryQueue = GetQueueClient(nameof(ExternalCountryQueue));

        private IQueueClient GetQueueClient(string queueName)
        {
            string queueConnectionString =
                this.configuration.GetConnectionString("ServiceBusConnection");

            return new QueueClient(queueConnectionString, queueName);
        }

        private MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            return Task.CompletedTask;
        }
    }
}
