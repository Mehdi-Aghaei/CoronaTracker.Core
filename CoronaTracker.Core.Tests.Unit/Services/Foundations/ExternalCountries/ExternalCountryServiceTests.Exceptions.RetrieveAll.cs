using System;
using System.Collections.Generic;
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
            var failedExternalCountryDependencyException =
                new FailedExternalCountryDependencyException(
                    criticalDependencyException);

            var expectedExternalCountryDependencyException =
                new ExternalCountryDependencyException(
                    failedExternalCountryDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllExternalCountriesAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<ExternalCountry>> getAllExternalCountriesTask =
                this.externalCountryService.RetrieveAllExternalCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryDependencyException>(() =>
                getAllExternalCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllExternalCountriesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedExternalCountryDependencyException))),
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
            var failedExternalCountryDependencyException =
                new FailedExternalCountryDependencyException(dependencyApiException);

            var expectedExternalCountryDependencyException =
                new ExternalCountryDependencyException(failedExternalCountryDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllExternalCountriesAsync())
                    .ThrowsAsync(dependencyApiException);

            // when
            ValueTask<List<ExternalCountry>> getAllExternalCountriesTask =
                this.externalCountryService.RetrieveAllExternalCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryDependencyException>(() =>
                getAllExternalCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllExternalCountriesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryDependencyException))),
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
                broker.GetAllExternalCountriesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<ExternalCountry>> getAllExternalCountriesTask =
                this.externalCountryService.RetrieveAllExternalCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryServiceException>(() =>
                getAllExternalCountriesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllExternalCountriesAsync(),
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
