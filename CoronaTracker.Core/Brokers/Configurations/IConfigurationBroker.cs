// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;

namespace CoronaTracker.Core.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        Guid GetTrustedSourceId();
    }
}
