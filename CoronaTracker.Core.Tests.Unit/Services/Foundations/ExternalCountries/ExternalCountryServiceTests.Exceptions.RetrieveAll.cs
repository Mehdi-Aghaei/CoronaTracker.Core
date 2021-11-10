using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountries.Exceptions;
using CoronaTrackerHungary.Web.Api.Models.Countries.Exceptions;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountries
{
    public partial class ExternalCountryServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalDependencyExceptionOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedCountryDependencyException =
                new FailedExternalCountryDependencyException(
                    criticalDependencyException);

            var expectedCountryDependencyException =
                new ExternalCountryDependencyException(
                    failedCountryDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCountriesAsync())
                .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<ExternalCountry>> getAllCountriesTask =
                this.externalCountryService.RetrieveAllCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryDependencyException>(() =>
                getAllCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCountriesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyApiExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
          Exception dependencyApiException)
        {
            // given 
            var failedCountryDependencyException =
                new FailedExternalCountryDependencyException(dependencyApiException);

            var expectedCountryDependencyException =
                new ExternalCountryDependencyException(failedCountryDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCountriesAsync())
                .ThrowsAsync(dependencyApiException);

            // when
            ValueTask<List<ExternalCountry>> getAllCountriesTask =
                this.externalCountryService.RetrieveAllCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryDependencyException>(() =>
                getAllCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCountriesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedExternalCountryServiceException =
                new FailedExternalCountryServiceException(serviceException);

            var expectedExternalCountryServiceException =
                new ExternalCountryServiceException(failedExternalCountryServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCountriesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<ExternalCountry>> getAllExternalCountriesTask =
                this.externalCountryService.RetrieveAllCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryServiceException>(() =>
                getAllExternalCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCountriesAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
