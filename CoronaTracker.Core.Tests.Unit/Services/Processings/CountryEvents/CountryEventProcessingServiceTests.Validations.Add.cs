// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCountryEventIsNullAndLogItAsync()
        {
            // given
            CountryEvent nullCountryEvent = null;
            var nullCountryEventException =
                new NullCountryEventProcessingException();

            var expectedCountryEventValidationException =
                new CountryEventProcessingValidationException(nullCountryEventException);

            // when
            ValueTask<CountryEvent> addCountryEventTask =
                this.countryEventProcessingService.AddCountryEventAsync(nullCountryEvent);

            // then
            await Assert.ThrowsAsync<CountryEventProcessingValidationException>(() =>
               addCountryEventTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventValidationException))),
                        Times.Once);

            this.countryEventServiceMock.Verify(service =>
                service.AddCountryEventAsync(It.IsAny<CountryEvent>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.countryEventServiceMock.VerifyNoOtherCalls();
        }
    }
}
