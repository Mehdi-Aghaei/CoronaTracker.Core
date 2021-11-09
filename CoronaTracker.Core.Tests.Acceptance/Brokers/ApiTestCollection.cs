using Xunit;

namespace CoronaTracker.Core.Tests.Acceptance.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<CoronaTrackerApiBroker>
    {
    }
}
