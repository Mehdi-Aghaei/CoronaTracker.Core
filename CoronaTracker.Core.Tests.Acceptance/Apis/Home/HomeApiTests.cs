// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using CoronaTracker.Core.Tests.Acceptance.Brokers;
using Xunit;

namespace CoronaTracker.Core.Tests.Acceptance.Apis.Home
{
    [Collection(nameof(ApiTestCollection))]
    public partial class HomeApiTests
    {
        private readonly CoronaTrackerApiBroker coronaTrackerApiBroker;

        public HomeApiTests(CoronaTrackerApiBroker coronaTrackerApiBroker) =>
            this.coronaTrackerApiBroker = coronaTrackerApiBroker;
    }
}
