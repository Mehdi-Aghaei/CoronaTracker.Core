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

            this.queueBrokerMock.Setup(broker => broker
                .EnqueueCountryMessageAsync(It.IsAny<Message>()))
                    .Throws(dependencyMessageQueueException);

            // when
            ValueTask<CountryEvent> addCountryEventTask =
                this.countryEventService.AddCountryEventAsync(someCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventDependencyException>(() =>
                addCountryEventTask.AsTask());

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
