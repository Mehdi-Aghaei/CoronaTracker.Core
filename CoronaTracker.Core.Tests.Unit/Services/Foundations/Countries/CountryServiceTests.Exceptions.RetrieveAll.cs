// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Models.Foundations.Countries.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedCountryStorageException =
                new FailedCountryStorageException(sqlException);

            var expectedCountryDependencyException =
                new CountryDependencyException(failedCountryStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCountries())
                    .Throws(sqlException);

            // when
            Action retrieveAllCountriesAction = () =>
                this.countryService.RetrieveAllCountries();

            // then
            Assert.Throws<CountryDependencyException>(
                retrieveAllCountriesAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            string exceptionMessage = GetRandomMessage();

            var serviceException = new Exception(exceptionMessage);

            var failedCountryServiceException =
                new FailedCountryServiceException(serviceException);

            var expectedCountryServiceException =
                new CountryServiceException(failedCountryServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCountries())
                    .Throws(serviceException);

            // when
            Action retrieveAllCountriesAction = () =>
                this.countryService.RetrieveAllCountries();

            // then
            Assert.Throws<CountryServiceException>(
                retrieveAllCountriesAction);


            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
