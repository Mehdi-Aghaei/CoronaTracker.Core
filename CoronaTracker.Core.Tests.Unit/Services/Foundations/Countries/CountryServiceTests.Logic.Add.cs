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

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldAddCountryAsync()
        {
            // given
            Country randomCountry = CreateRandomCountry();
            Country inputCountry = randomCountry;
            Country storageCountry = inputCountry;
            Country expectedCountry = storageCountry.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCountryAsync(inputCountry))
                    .ReturnsAsync(storageCountry);

            // when
            Country actualCountry = 
                await this.countryService.AddCountryAsync(inputCountry);

            // then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(inputCountry),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
