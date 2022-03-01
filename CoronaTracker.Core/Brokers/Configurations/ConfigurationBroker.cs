// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.Configuration;

namespace CoronaTracker.Core.Brokers.Configurations
{
    public class ConfigurationBroker : IConfigurationBroker
    {
        private readonly IConfiguration configuration;

        public ConfigurationBroker(IConfiguration configuration) =>
            this.configuration = configuration;

        public Guid GetTrustedSourceId() =>
            this.configuration.GetValue<Guid>("TrustedSourceId");
    }
}
