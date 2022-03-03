// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.CountryEvents
{
    public partial class CountryEventServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyMessageQueueExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyMessageQueueException)
        {
            // given
            CountryEvent someCountryEvent = CreateRandomCountryEvent();

            var failedCountryEventDependencyException =
                new FailedCountryEventDependencyException(criticalDependencyMessageQueueException);

            var expectedCountryEventDependencyException =
                new CountryEventDependencyException(failedCountryEventDependencyException);

            this.queueBrokerMock.Setup(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()))
                    .Throws(criticalDependencyMessageQueueException);
            // when
            ValueTask<CountryEvent> countryEventTask =
                this.countryEventService.AddCountryEventAsync(someCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventDependencyException>(() =>
                countryEventTask.AsTask());

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyMessageQueueExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception dependencyMessageQueueException)
        {
            // given
            CountryEvent someCountryEvent = CreateRandomCountryEvent();

            var failedCountryEventDependencyException =
                new FailedCountryEventDependencyException(
                    dependencyMessageQueueException);

            var expectedCountryEventDependencyException =
                new CountryEventDependencyException(
                    failedCountryEventDependencyException);

            this.configuratinBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId())
                    .Throws(dependencyMessageQueueException);

            // when
            ValueTask<CountryEvent> addCountryEventTask =
                this.countryEventService.AddCountryEventAsync(someCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventDependencyException>(() =>
                addCountryEventTask.AsTask());

            this.configuratinBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.configuratinBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        } 

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            CountryEvent someCountryEvent = CreateRandomCountryEvent();
            var serviceException = new Exception();

            var failedCountryEventServiceException =
                new FailedCountryEventServiceException(serviceException);

            var expectedCountryEventServiceException =
                new CountryEventServiceException(failedCountryEventServiceException);

            this.queueBrokerMock.Setup(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()))
                    .Throws(serviceException);

            // when
            ValueTask<CountryEvent> addCountryEventTask =
                this.countryEventService.AddCountryEventAsync(someCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventServiceException>(() =>
                addCountryEventTask.AsTask());

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventServiceException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
