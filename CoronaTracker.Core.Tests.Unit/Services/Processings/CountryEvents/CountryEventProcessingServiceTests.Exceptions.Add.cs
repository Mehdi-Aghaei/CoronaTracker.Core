// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            var someCountryEvent = CreateRandomCountryEvent();

            var expectedCountryEventProcessingDependencyException =
                new CountryEventProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.countryEventServiceMock.Setup(service =>
                service.AddCountryEventAsync(someCountryEvent))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<CountryEvent> addcountryEventTask =
                this.countryEventProcessingService.AddCountryEventAsync(someCountryEvent);
                
            // then
            await Assert.ThrowsAsync<CountryEventProcessingDependencyException>(() =>
                addcountryEventTask.AsTask());

            this.countryEventServiceMock.Verify(service =>
                service.AddCountryEventAsync(someCountryEvent),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventProcessingDependencyException))), 
                        Times.Once);

            this.countryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            CountryEvent someCountryEvent = CreateRandomCountryEvent();

            var serviceException = new Exception();

            var failedCountryEventProccesingServiceException =
                new FailedCountryEventProccesingServiceException(serviceException);

            var expectedCountryEventProccesingServiceException =
                new CountryEventProccesingServiceException(
                    failedCountryEventProccesingServiceException);

            this.countryEventServiceMock.Setup(service =>
                service.AddCountryEventAsync(someCountryEvent))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<CountryEvent> addCountryEventTask =
                this.countryEventProcessingService.AddCountryEventAsync(someCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventProccesingServiceException>(() =>
                addCountryEventTask.AsTask());

            this.countryEventServiceMock.Verify(service =>
                service.AddCountryEventAsync(someCountryEvent),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventProccesingServiceException))), 
                        Times.Once);

            this.countryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }

}
