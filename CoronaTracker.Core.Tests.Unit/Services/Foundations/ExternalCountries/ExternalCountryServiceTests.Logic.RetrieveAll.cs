using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
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
            List<Country> randomCountries = CreateRandomExternalCountries();
            List<Country> apiCountries = randomCountries;
            List<Country> expectedExternalCountries = apiCountries.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCountriesAsync())
                    .ReturnsAsync(apiCountries);

            // when
            List<Country> retrievedCountries =
                await this.externalCountryService.RetrieveAllCountriesAsync();

            // then
            retrievedCountries.Should().BeEquivalentTo(expectedExternalCountries);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCountriesAsync(), 
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
