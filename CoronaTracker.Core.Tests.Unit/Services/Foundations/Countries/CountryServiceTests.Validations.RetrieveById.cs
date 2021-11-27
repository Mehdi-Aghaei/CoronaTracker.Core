// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidCountryId = Guid.Empty;

            var invalidCountryException =
                new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.Id),
                values: "Id is required");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            // when
            ValueTask<Country> retrieveByIdCountryTask =
                this.countryService.RetrieveCountryByIdAsync(invalidCountryId);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                retrieveByIdCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfCountryIsNotFoundAndLogItAsyn()
        {
            // given
            Guid someCountryId = Guid.NewGuid();
            Country noCountry = null;

            var notFoundCountryException =
                new NotFoundCountryException(someCountryId);

            var expectedCountryValidationException =
                new CountryValidationException(notFoundCountryException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(someCountryId))
                    .ReturnsAsync(noCountry);

            // when
            ValueTask<Country> retrieveByIdCountryTask =
                this.countryService.RetrieveCountryByIdAsync(someCountryId);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                retrieveByIdCountryTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(someCountryId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
