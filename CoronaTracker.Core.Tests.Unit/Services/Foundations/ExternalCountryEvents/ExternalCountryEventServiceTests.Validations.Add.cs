// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

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
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfExternalCountryEventIsNullAndLogItAsync()
        {
            // given
            ExternalCountryEvent nullExternalCountryEvent = null;

            var nullExternalCountryEventException =
                new NullExternalCountryEventException();

            var expectedExternalCountryEventValidationException =
                new ExternalCountryEventValidationException(nullExternalCountryEventException);

            // when
            ValueTask<ExternalCountryEvent> externalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(nullExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventValidationException>(() =>
                externalCountryEventTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventValidationException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
