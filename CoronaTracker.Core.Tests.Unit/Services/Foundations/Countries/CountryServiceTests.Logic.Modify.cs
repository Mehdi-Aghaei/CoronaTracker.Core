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
        public async Task ShouldModifyCountryAsync()
        {
            // given
            Country randomCountry = CreateRandomCountry();
            Country inputCountry = randomCountry;
            Country storageCountry = inputCountry.DeepClone();
            Country updatedCountry = inputCountry;
            Country expectedCountry = updatedCountry.DeepClone();
            Guid countryId = inputCountry.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(countryId))
                .ReturnsAsync(storageCountry);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateCountryAsync(inputCountry))
                    .ReturnsAsync(updatedCountry);

            // when 
            Country actualCountry =
                await this.countryService.ModifyCountryAsync(inputCountry);

            // then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(countryId),
                    Times.Once);
            
            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(inputCountry),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
