// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Country someCountry = CreateRandomCountry();
            SqlException sqlException = GetSqlException();

            var failedCountryStorageException =
                new FailedCountryStorageException(sqlException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(sqlException);

            // then
            ValueTask<Country> addCountryTask =
                this.countryService.ModifyCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
                addCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
             broker.SelectCountryByIdAsync(someCountry.Id),
                 Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            Country randomCountry = CreateRandomCountry();
            var databaseUpdateException = new DbUpdateException();

            var failedCountryException =
                new FailedCountryStorageException(databaseUpdateException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(randomCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(randomCountry.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(randomCountry),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            Country randomCountry = CreateRandomCountry();
            var databaseUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedCountryException =
                new LockedCountryException(databaseUpdateConcurrencyException);

            var expectedCountryDependencyValidationException =
                new CountryDependencyValidationException(lockedCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateConcurrencyException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(randomCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(randomCountry.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(randomCountry),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            int minuteInPast = GetRandomNegativeNumber();
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Country randomCountry = CreateRandomCountry(randomDateTime);
            Country someCountry = randomCountry;
            someCountry.CreatedDate = randomDateTime.AddMinutes(minuteInPast);
            var serviceException = new Exception();

            var failedCountryException =
                new FailedCountryServiceException(serviceException);

            var expectedCountryServiceException =
                new CountryServiceException(failedCountryException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(someCountry.Id))
                    .ThrowsAsync(serviceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryServiceException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(someCountry.Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
