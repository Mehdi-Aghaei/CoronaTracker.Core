// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.ExternalCountryEvents
{
    public partial class ExternalCountryEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldAddExternalCountryEventAsync()
        {
            // given
            ExternalCountryEvent randomExternalCountryEvent = CreateRandomExternalCountryEvent();
            ExternalCountryEvent inputExternalCountryEvent = randomExternalCountryEvent;
            ExternalCountryEvent addedExternalCountryEvent = inputExternalCountryEvent;
            ExternalCountryEvent expectedExternalCountryEvent = addedExternalCountryEvent.DeepClone();

            this.externalCountryEventServiceMock.Setup(service =>
                service.AddExternalCountryEventAsync(inputExternalCountryEvent))
                    .ReturnsAsync(addedExternalCountryEvent);

            // when
            ExternalCountryEvent actualExternalCountryEvent =
                await this.externalCountryEventProcessingService
                    .AddExternalCountryEventAsync(inputExternalCountryEvent);

            // then
            actualExternalCountryEvent.Should().BeEquivalentTo(expectedExternalCountryEvent);

            this.externalCountryEventServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(inputExternalCountryEvent),
                    Times.Once);

            this.externalCountryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
