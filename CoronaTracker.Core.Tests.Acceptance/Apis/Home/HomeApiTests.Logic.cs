// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CoronaTracker.Core.Tests.Acceptance.Apis.Home
{
    public partial class HomeApiTests
    {
        [Fact]
        public async Task ShouldReturnHomeMessageAsync()
        {
            // given
            string expectedHomeMessage =
                "Hi dear, you have to check countries' API :)";

            // when
            string actualHomeMessage =
                await this.coronaTrackerApiBroker.GetHomeMessageAsync();

            // then
            actualHomeMessage.Should().BeEquivalentTo(expectedHomeMessage);
        }
    }
}
