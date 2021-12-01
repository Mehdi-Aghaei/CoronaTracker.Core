// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        [Fact]
        public async Task ShouldAddCountryIfNotExistAsync()
        {
            // given
            IQueryable<Country> randomCountries =
                CreateRandomCountries();

            IQueryable<Country> retrievedCountries =
                randomCountries;

            Country randomCountry = CreateRandomCountry();
            Country inputCountry = randomCountry;
            Country addedCountry = inputCountry;
            Country expectedCountry = addedCountry.DeepClone();

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Returns(retrievedCountries);

            this.countryServiceMock.Setup(service =>
                service.AddCountryAsync(inputCountry))
                    .ReturnsAsync(addedCountry);

            // when
            Country actualCountry = await this.countryProcessingService
                .UpsertCountryAsync(inputCountry);

            // then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(inputCountry),
                    Times.Once);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
