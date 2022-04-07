// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Foundations.Countries.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someCountryId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var failedCountryStorageException =
                new FailedCountryStorageException(sqlException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Country> retrieveCountryByIdTask =
                this.countryService.RetrieveCountryByIdAsync(someCountryId);

            // then
            await Assert.ThrowsAsync<CountryDependencyException>(() =>
                retrieveCountryByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someCountryId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedCountryServiceException =
                new FailedCountryServiceException(serviceException);

            var expectedCountryServiceException =
                new CountryServiceException(failedCountryServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Country> retrieveCountryByIdTask =
                this.countryService.RetrieveCountryByIdAsync(someCountryId);

            // then
            await Assert.ThrowsAsync<CountryServiceException>(() =>
                retrieveCountryByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCountryByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedCountryServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
