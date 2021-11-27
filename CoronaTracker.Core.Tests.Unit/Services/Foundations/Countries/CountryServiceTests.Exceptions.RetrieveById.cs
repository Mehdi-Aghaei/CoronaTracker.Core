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
    }
}
