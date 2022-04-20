// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace CoronaTracker.Core.Tests.Acceptance.Brokers
{
    public partial class CoronaTrackerApiBroker
    {
        private const string HomeRelativeUrl = "api/home";

        public async ValueTask<string> GetHomeMessageAsync() =>
            await this.apiFactoryClient.GetContentStringAsync(HomeRelativeUrl);
    }
}
