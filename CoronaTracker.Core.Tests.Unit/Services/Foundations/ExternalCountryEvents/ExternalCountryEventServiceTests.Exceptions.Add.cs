// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventServiceTests
    {
        [Theory]
        [MemberData(nameof(MessageQueueExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception messageQueueException)
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var failedExternalCountryEventDependencyException =
                new FailedExternalCountryEventDependencyException(
                    messageQueueException);

            var expectedExternalCountryEventDependencyException =
                new ExternalCountryEventDependencyException(
                    failedExternalCountryEventDependencyException);

            this.configurationBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(messageQueueException);

            // when
            ValueTask<ExternalCountryEvent> externalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventDependencyException>(() =>
                externalCountryEventTask.AsTask());

            this.configurationBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedExternalCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueExternalCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configurationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MessageQueueDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception messageQueueDependencyException)
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();

            var failedExternalCountryEventDependencyException =
                new FailedExternalCountryEventDependencyException(
                    messageQueueDependencyException);

            var expectedExternalCountryEventDependencyException =
                new ExternalCountryEventDependencyException(
                    failedExternalCountryEventDependencyException);

            this.configurationBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(messageQueueDependencyException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventDependencyException>(() =>
                addExternalCountryEventTask.AsTask());

            this.configurationBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueExternalCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configurationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfArgumentExceptionOccursAndLogItAsync()
        {
            // given
            ExternalCountryEvent someExternalCountryEvent = CreateRandomExternalCountryEvent();
            var argumentException = new ArgumentException();

            var invalidExternalCountryEventArgumentExcpetion =
                new InvalidExternalCountryEventArgumentException(argumentException);

            var expectedExternalCountryEventDependencyValidationException =
                new ExternalCountryEventDependencyValidationException(
                    invalidExternalCountryEventArgumentExcpetion);

            this.configurationBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(argumentException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventDependencyValidationException>(() =>
                addExternalCountryEventTask.AsTask());

            this.configurationBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventDependencyValidationException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueExternalCountryMessageAsync(
                    It.IsAny<Message>()), Times.Never);

            this.configurationBrokerMock.VerifyNoOtherCalls();
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

            this.configurationBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(serviceException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(someExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventServiceException>(() =>
                addExternalCountryEventTask.AsTask());

            this.configurationBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventServiceException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueExternalCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.configurationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
