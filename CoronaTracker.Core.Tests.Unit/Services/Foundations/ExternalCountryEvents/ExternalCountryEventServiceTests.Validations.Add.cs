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
                broker.EnqueueExternalCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
            this.configurationBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfExternalCountryEventIsInvalidAndLogItAsync()
        {
            // given
            var invalidExternalCountryEvent = new ExternalCountryEvent();
            var invalidExternalCountryEventException = new InvalidExternalCountryEventException();

            invalidExternalCountryEventException.AddData(
                key: nameof(ExternalCountryEvent.ExternalCountry),
                values: "Object is required");

            var expectedExternalCountryEventValidationException =
                new ExternalCountryEventValidationException(invalidExternalCountryEventException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventService.AddExternalCountryEventAsync(invalidExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventValidationException>(() =>
                addExternalCountryEventTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventValidationException))),
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
