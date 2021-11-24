using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountries
{
    public partial class ExternalCountryServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllExternalCountriesAsync()
        {
            // given
            List<ExternalCountry> randomCountries = CreateRandomExternalCountries();
            List<ExternalCountry> externalCountries = randomCountries;
            List<ExternalCountry> expectedExternalCountries = externalCountries.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllExternalCountriesAsync())
                    .ReturnsAsync(externalCountries);

            // when
            List<ExternalCountry> retrieveAllCountries =
                await this.externalCountryService.RetrieveAllExternalCountriesAsync();

            // then
            retrieveAllCountries.Should().BeEquivalentTo(expectedExternalCountries);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllExternalCountriesAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
