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
        public async Task ShouldAddCountryAsync()
        {
            // given
            DateTimeOffset dateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(dateTime);
            Country inputCountry = randomCountry;
            Country storageCountry = inputCountry;
            Country expectedCountry = storageCountry.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCountryAsync(inputCountry))
                    .ReturnsAsync(storageCountry);

            // when
            Country actualCountry =
                await this.countryService.AddCountryAsync(inputCountry);

            // then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(inputCountry),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
