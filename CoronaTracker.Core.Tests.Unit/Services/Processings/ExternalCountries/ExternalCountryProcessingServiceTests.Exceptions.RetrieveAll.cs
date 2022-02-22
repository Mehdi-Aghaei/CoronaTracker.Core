// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.Processings.ExternalCountries;
using CoronaTracker.Core.Models.Processings.ExternalCountries.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountries
{
    public partial class ExternalCountryProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given 
            var randomExternalCountries = CreateRandomExternalCountries();

            var expectedExternalCountryProcessingDependencyException =
                new ExternalCountryProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.externalCountryServiceMock.Setup(service =>
                service.RetrieveAllExternalCountriesAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<ExternalCountry>> retrievedCountriesTask =
                this.externalCountryProcessingService.RetrieveAllExternalCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryProcessingDependencyException>(() =>
                retrievedCountriesTask.AsTask());

            this.externalCountryServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryProcessingDependencyException))),
                        Times.Once);

            this.externalCountryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someExternalCountries = CreateRandomExternalCountries();
            var serviceException = new Exception();

            var failedExternalCountryProcessingServiceException =
                new FailedExternalCountryProcessingServiceException(serviceException);

            var expectedExternalCountryProcessingServiceException =
                new ExternalCountryProcessingServiceException(
                    failedExternalCountryProcessingServiceException);

            this.externalCountryServiceMock.Setup(service =>
                service.RetrieveAllExternalCountriesAsync())
                    .Throws(serviceException);

            // when
            ValueTask<List<ExternalCountry>> retrieveAllExternalCountryTask =
                this.externalCountryProcessingService.RetrieveAllExternalCountriesAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryProcessingServiceException>(() =>
               retrieveAllExternalCountryTask.AsTask());

            this.externalCountryServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                     expectedExternalCountryProcessingServiceException))),
                        Times.Once);

            this.externalCountryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
