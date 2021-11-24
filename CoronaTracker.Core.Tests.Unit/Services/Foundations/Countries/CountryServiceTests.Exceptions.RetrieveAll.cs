using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
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
    }
}
