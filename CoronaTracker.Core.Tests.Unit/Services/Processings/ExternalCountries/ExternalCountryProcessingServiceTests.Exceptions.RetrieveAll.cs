// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.Processings.ExternalCountries;
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
            var someCountries = CreateRandomExternalCountries();

            var ExpectedExternalCountryProcessingDependencyException =
                new ExternalCountryProcessingDependencyException(dependencyException);

            this.externalCountryServiceMock.Setup(service =>
                service.RetrieveAllExternalCountriesAsync())
                .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<ExternalCountry>> retrievedCountriesTask =
                this.externalCountryProcessingService.RetrieveAllExternalCountriesProcessingAsync();

            // then
            await Assert.ThrowsAsync<ExternalCountryProcessingDependencyException>(() =>
                retrievedCountriesTask.AsTask());

            this.externalCountryServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    ExpectedExternalCountryProcessingDependencyException))),
                        Times.Once);

            this.externalCountryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
