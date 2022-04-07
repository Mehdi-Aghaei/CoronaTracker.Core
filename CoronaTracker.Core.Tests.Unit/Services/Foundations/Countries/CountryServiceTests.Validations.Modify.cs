// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Foundations.Countries.Exceptions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfCountryIsNullAndLogItAsync()
        {
            //given
            Country nullcountry = null;
            var nullCountryException = new NullCountryException();

            var expectedCountryValdationException =
                new CountryValidationException(nullCountryException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(nullcountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValdationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfCountryIsInvalidAndLogItAsync(string invalidText)
        {
            // given 
            var invalidCountry = new Country
            {
                Name = invalidText,
                Iso3 = invalidText,
                Continent = invalidText
            };

            var invalidCountryException = new InvalidCountryException();

            invalidCountryException.AddData(
               key: nameof(Country.Id),
               values: "Id is required");

            invalidCountryException.AddData(
                key: nameof(Country.Name),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.Iso3),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.Continent),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.CreatedDate),
                values: "Date is required");

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: new[] {
                    "Date is required",
                    $"Date is the same as {nameof(Country.CreatedDate)}"
                });

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTimeOffset(),
                   Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsSameAsCreatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(randomDateTime);
            Country invalidCountry = randomCountry;
            var invalidCountryException = new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: $"Date is the same as {nameof(Country.CreatedDate)}");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(invalidCountry.Id),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsNotRecentAndLogItAsync(int minutes)
        {
            // given
            DateTimeOffset dateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(dateTime);
            Country inputCountry = randomCountry;
            inputCountry.UpdatedDate = dateTime.AddMinutes(minutes);
            var invalidCountryException =
                new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: "Date is not recent");

            var expectedCountryValidatonException =
                new CountryValidationException(invalidCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                .Returns(dateTime);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(inputCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidatonException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfCountryDoesNotExistAndLogItAsync()
        {
            // given
            int randomNegativeMinutes = GetRandomNegativeNumber();
            DateTimeOffset dateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(dateTime);
            Country nonExistCountry = randomCountry;
            nonExistCountry.CreatedDate = dateTime.AddMinutes(randomNegativeMinutes);
            Country nullCountry = null;

            var notFoundCountryException =
                new NotFoundCountryException(nonExistCountry.Id);

            var expectedCountryValidationException =
                new CountryValidationException(notFoundCountryException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(nonExistCountry.Id))
                .ReturnsAsync(nullCountry);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                .Returns(dateTime);

            // when 
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(nonExistCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(nonExistCountry.Id),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfStorageCreatedDateNotSameAsCreatedDateAndLogItAsync()
        {
            // given
            int randomNumber = GetRandomNumber();
            int randomMinutes = randomNumber;
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(randomDateTime);
            Country invalidCountry = randomCountry;
            invalidCountry.UpdatedDate = randomDateTime;
            Country storageCountry = randomCountry.DeepClone();
            Guid countryId = invalidCountry.Id;
            invalidCountry.CreatedDate = storageCountry.CreatedDate.AddMinutes(randomMinutes);
            var invalidCountryException = new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.CreatedDate),
                values: $"Date is not the same as {nameof(Country.CreatedDate)}");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(countryId))
                    .ReturnsAsync(storageCountry);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(invalidCountry.Id),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfStorageUpdatedDateSameAsUpdatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomModifyCountry(randomDateTime);
            Country invalidCountry = randomCountry;

            Country storageCountry = randomCountry.DeepClone();
            invalidCountry.UpdatedDate = storageCountry.UpdatedDate;

            var invalidCountryException = new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: $"Date is the same as {nameof(Country.UpdatedDate)}");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(invalidCountry.Id))
                .ReturnsAsync(storageCountry);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(invalidCountry.Id),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
