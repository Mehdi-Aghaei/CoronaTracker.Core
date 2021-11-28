// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
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
        public async Task ShouldRetrieveCountryByIdAsync()
        {
            // given
            Guid randomCountryId = Guid.NewGuid();
            Guid inputCountryId = randomCountryId;
            Country someCountry = CreateRandomCountry();
            Country storageCountry = someCountry;
            Country expectedCountry = storageCountry.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(inputCountryId))
                    .ReturnsAsync(storageCountry);
            // when
            Country actualCountry =
                await this.countryService.RetrieveCountryByIdAsync(inputCountryId);

            // then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.storageBrokerMock.Verify(brokers =>
                brokers.SelectCountryByIdAsync(inputCountryId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
