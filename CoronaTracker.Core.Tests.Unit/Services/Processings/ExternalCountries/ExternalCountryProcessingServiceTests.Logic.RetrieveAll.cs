﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;
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
            List<ExternalCountry> randomExternalCountries = CreateRandomExternalCountries();
            List<ExternalCountry> externalCountries = randomExternalCountries;
            List<ExternalCountry> expectedExternalCountries = externalCountries.DeepClone();

            this.externalCountryServiceMock.Setup(service =>
                service.RetrieveAllExternalCountriesAsync())
                    .ReturnsAsync(externalCountries);

            // when
            List<ExternalCountry> retrievedExternalCountries =
                await this.externalCountryProcessingService.RetrieveAllExternalCountriesAsync();

            // then
            retrievedExternalCountries.Should().BeEquivalentTo(expectedExternalCountries);

            this.externalCountryServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.Once());

            this.externalCountryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
