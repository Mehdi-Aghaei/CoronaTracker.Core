// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventServiceTests
    {
        [Fact]
        public async Task ShouldAddExternalCountryEventAsync()
        {
            // given
            ExternalCountryEvent randomExternalCountryEvent = CreateRandomExternalCountryEvent();
            ExternalCountryEvent inputExternalCountryEvent = randomExternalCountryEvent;
            ExternalCountryEvent expectedExternalCountryEvent = inputExternalCountryEvent.DeepClone();
            Guid randomTrustedSourceId = Guid.NewGuid();
            Guid givenTrustedSourceId = randomTrustedSourceId;
            expectedExternalCountryEvent.TrustedSourceId = givenTrustedSourceId;

            string serializedExternalCountryEvent =
                JsonSerializer.Serialize(expectedExternalCountryEvent);

            var expectedExternalCountryEventMessage = new Message
            {
                Body = Encoding.UTF8.GetBytes(serializedExternalCountryEvent)
            };

            this.configurationBrokerMock.Setup(broker =>
                broker.GetTrustedSourceId()).Returns(givenTrustedSourceId);

            // when
            ExternalCountryEvent actualExternalCountryEvent =
                await this.externalCountryEventService.AddExternalCountryEventAsync(inputExternalCountryEvent);

            // then
            actualExternalCountryEvent.Should().BeEquivalentTo(expectedExternalCountryEvent);

            this.configurationBrokerMock.Verify(broker =>
                broker.GetTrustedSourceId(),
                    Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.EnqueueExternalCountryMessageAsync(
                    It.Is(SameMessageAs(
                        expectedExternalCountryEventMessage))),
                            Times.Once);

            this.configurationBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
