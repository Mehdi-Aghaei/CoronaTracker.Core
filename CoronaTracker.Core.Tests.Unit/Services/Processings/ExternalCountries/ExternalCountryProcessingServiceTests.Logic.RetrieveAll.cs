// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountries
{
    public partial class ExternalCountryProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllExternalCountriesAsync()
        {
            // given
            List<ExternalCountry> randomCountries = CreateRandomExternalCountries();
            List<ExternalCountry> externalCountries = randomCountries;
            List<ExternalCountry> expectedExternalCountries = externalCountries.DeepClone();
            this.externalCountryServiceMock.Setup(service =>
                service.RetrieveAllExternalCountriesAsync())
                .ReturnsAsync(externalCountries);

            // when
            List<ExternalCountry> retrievedCountries =
                await this.externalCountryProcessingService.RetrieveAllExternalCountriesAsync();

            // then
            retrievedCountries.Should().BeEquivalentTo(expectedExternalCountries);

            this.externalCountryServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.Once());

            this.externalCountryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
