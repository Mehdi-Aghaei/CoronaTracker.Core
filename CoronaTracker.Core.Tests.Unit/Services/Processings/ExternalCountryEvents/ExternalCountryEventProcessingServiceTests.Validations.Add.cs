// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountryEvents
{
    public partial class ExternalCountryEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfExternalCountryEventIsNullAndLogItAsync()
        {
            // given
            ExternalCountryEvent nullExternalCountryEvent = null;
            var nullExternalCountryEventException =
                new NullExternalCountryEventProcessingException();

            var expectedExternalCountryEventValidationException =
                new ExternalCountryEventProcessingValidationException(nullExternalCountryEventException);

            // when
            ValueTask<ExternalCountryEvent> addExternalCountryEventTask =
                this.externalCountryEventProcessingService.AddExternalCountryEventAsync(nullExternalCountryEvent);

            // then
            await Assert.ThrowsAsync<ExternalCountryEventProcessingValidationException>(() =>
               addExternalCountryEventTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedExternalCountryEventValidationException))),
                        Times.Once);

            this.externalCountryEventServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(It.IsAny<ExternalCountryEvent>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.externalCountryEventServiceMock.VerifyNoOtherCalls();
        }
    }
}
