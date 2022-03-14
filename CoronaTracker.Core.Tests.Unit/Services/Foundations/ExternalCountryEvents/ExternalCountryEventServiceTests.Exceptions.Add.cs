// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyMessageQueueExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyMessageQueueException)
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var failedExternalCountryEventDependencyException =
                new FailedExternalCountryEventDependencyException(criticalDependencyMessageQueueException);

            var expectedExternalCountryEventDependencyException =
                new ExternalCountryEventDependencyException(failedExternalCountryEventDependencyException);

            this.configuratinBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(criticalDependencyMessageQueueException);

            // when
            ValueTask<ExternalCountryEvent> externalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventDependencyException>(() =>
                externalCountryEventTask.AsTask());

            this.configuratinBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedExternalCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configuratinBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyMessageQueueExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception dependencyMessageQueueException)
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var failedExternalCountryEventDependencyException =
                new FailedExternalCountryEventDependencyException(
                    dependencyMessageQueueException);

            var expectedExternalCountryEventDependencyException =
                new ExternalCountryEventDependencyException(
                    failedExternalCountryEventDependencyException);

            this.configuratinBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(dependencyMessageQueueException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventDependencyException>(() =>
                addExternalCountryEventTask.AsTask());

            this.configuratinBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configuratinBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();
            var serviceException = new Exception();

            var failedExternalCountryEventServiceException =
                new FailedExternalCountryEventServiceException(serviceException);

            var expectedExternalCountryEventServiceException =
                new ExternalCountryEventServiceException(failedExternalCountryEventServiceException);

            this.configuratinBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(serviceException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventServiceException>(() =>
                addExternalCountryEventTask.AsTask());

            this.configuratinBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventServiceException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configuratinBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
