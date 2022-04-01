// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountryEvents
{
    public partial class ExternalCountryEventProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfValidationErrorOccursAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var expectedExternalCountryEventProcessingDependencyValidationException =
                new ExternalCountryEventProcessingDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.externalCountryEventServiceMock.Setup(broker =>
                broker.AddExternalCountryEventAsync(It.IsAny<ExternalCountryEvent>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventProcessingService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventProcessingDependencyValidationException>(() =>
                addExternalCountryEventTask.AsTask());

            this.externalCountryEventServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(It.IsAny<ExternalCountryEvent>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventProcessingDependencyValidationException))),
                        Times.Once);

            this.externalCountryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            var someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var expectedExternalCountryEventProcessingDependencyException =
                new ExternalCountryEventProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.externalCountryEventServiceMock.Setup(service =>
                service.AddExternalCountryEventAsync(someExternalCountryEvent))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventProcessingService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventProcessingDependencyException>(() =>
                addExternalCountryEventTask.AsTask());

            this.externalCountryEventServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(someExternalCountryEvent),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventProcessingDependencyException))),
                        Times.Once);

            this.externalCountryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var serviceException = new Exception();

            var failedExternalCountryEventProccesingServiceException =
                new FailedExternalCountryEventProcessingServiceException(serviceException);

            var expectedExternalCountryEventProccesingServiceException =
                new ExternalCountryEventProccesingServiceException(
                    failedExternalCountryEventProccesingServiceException);

            this.externalCountryEventServiceMock.Setup(service =>
                service.AddExternalCountryEventAsync(someExternalCountryEvent))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventProcessingService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventProccesingServiceException>(() =>
                addExternalCountryEventTask.AsTask());

            this.externalCountryEventServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(someExternalCountryEvent),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventProccesingServiceException))),
                        Times.Once);

            this.externalCountryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
