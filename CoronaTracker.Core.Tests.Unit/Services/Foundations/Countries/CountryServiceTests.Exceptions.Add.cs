using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Country someCountry = CreateRandomCountry();
            SqlException sqlException = GetSqlException();

            var failedCountryException =
                new FailedCountryStorageException(sqlException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(sqlException);

            // then
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
                addCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfCountryAlreadyExsitsAndLogItAsync()
        {
            // given
            Country randomCountry = CreateRandomCountry();
            Country alreadyExistsCountry = randomCountry;
            string randomMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(randomMessage);

            var alreadyExistsCountryException =
                new AlreadyExistsCountryException(duplicateKeyException);

            var expectedCountryDependencyValidationException =
                new CountryDependencyValidationException(alreadyExistsCountryException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(alreadyExistsCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyValidationException>(() =>
                addCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseErrorOccursAndLogItAsync()
        {
            // given
            Country someCountry = CreateRandomCountry();

            var databseUpdateException =
                new DbUpdateException();

            var failedCountryStorageException =
                new FailedCountryStorageException(databseUpdateException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databseUpdateException);

            // when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
                addCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTimeOffset(),
                   Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfDatabaseErrorOccursAndLogItAsync()
        {
            // given
            Country someCountry = CreateRandomCountry();
            var serviceException = new Exception();

            var failedCountryServiceExcepiton =
                new FailedCountryServiceException(serviceException);

            var expectedCountryServiceException =
                new CountryServiceException(failedCountryServiceExcepiton);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(serviceException);

            // when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(someCountry);

            //then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
               addCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTimeOffset(),
                   Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryServiceException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
