// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.CountryEvents.Exceptions;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.CountryEvents
{
    public partial class CountryEventServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCountryEventIsNullAndLogItAsync()
        {
            // given
            CountryEvent nullCountry = null;

            var nullCountryEventException =
                new NullCountryEventException();

            var expectedCountryEventValidationException =
                new CountryEventValidationException(nullCountryEventException);

            // when
            ValueTask<CountryEvent> countryEventTask =
                this.countryEventService.AddCountryEventAsync(nullCountry);

            // then
            await Assert.ThrowsAsync<CountryEventValidationException>(() =>
                countryEventTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryEventValidationException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueCountryMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
