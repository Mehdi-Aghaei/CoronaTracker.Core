// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnUpsertIfCountryIsNullAndLogItAsync()
        {
            // given
            Country nullCountry = null;

            var nullCountryProcessingException =
                new NullCountryProcessingException();

            var expectedCountryProcessingValidationException =
                new CountryProcessingValidationException(
                    nullCountryProcessingException);

            // when
            ValueTask<Country> upsertCountryTask =
                this.countryProcessingService.UpsertCountryAsync(
                    nullCountry);

            // then
            await Assert.ThrowsAsync<CountryProcessingValidationException>(() =>
               upsertCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryProcessingValidationException))),
                        Times.Once);

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.ModifyCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.countryServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnUpsertIfCountryIdIsInvalidAndLogItAsync()
        {
            // given
            var invalidCountry = CreateRandomCountry();
            invalidCountry.Id = Guid.Empty;

            var invalidCountryProcessingException =
                new InvalidCountryProcessingException();

            invalidCountryProcessingException.AddData(
                key: nameof(Country.Id),
                values: "Id is required");

            var expectedCountryProcessingValidationException =
                new CountryProcessingValidationException(invalidCountryProcessingException);

            // when
            ValueTask<Country> upsertCountryTask =
                this.countryProcessingService.UpsertCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryProcessingValidationException>(() =>
                upsertCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
              broker.LogError(It.Is(SameExceptionAs(
                  expectedCountryProcessingValidationException))),
                      Times.Once);

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.ModifyCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.countryServiceMock.VerifyNoOtherCalls();
        }
    }
}
