// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
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
        public void ShouldListenToExternalCountryEvent()
        {
            // given
            var externalCountryEventHandlerMock = new Mock<Func<ExternalCountry, ValueTask>>();
            ExternalCountry randomExternalCountry = CreateRandomExternalCountry();
            ExternalCountry incomingExternalCountry = randomExternalCountry;

            string serializedExternalCountryEvent =
                JsonSerializer.Serialize(incomingExternalCountry);

            var externalCountryEventMessage = new Message
            {
                Body = Encoding.UTF8.GetBytes(serializedExternalCountryEvent)
            };

            this.queueBrokerMock.Setup(broker =>
               broker.ListenToExternalCountriesQueue(
                   It.IsAny<Func<Message, CancellationToken, Task>>()))
                       .Callback<Func<Message, CancellationToken, Task>>(eventFunction =>
                           eventFunction.Invoke(externalCountryEventMessage, It.IsAny<CancellationToken>()));
            // when
            this.externalCountryEventService.ListenToExternalCountriesEvent(externalCountryEventHandlerMock.Object);

            // then
            externalCountryEventHandlerMock.Verify(handler =>
                handler.Invoke(It.Is(
                    SameExternalCountryAs(incomingExternalCountry))),
                        Times.Once());

            this.queueBrokerMock.Verify(broker =>
               broker.ListenToExternalCountriesQueue(
                   It.IsAny<Func<Message, CancellationToken, Task>>()),
                       Times.Once());

            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
