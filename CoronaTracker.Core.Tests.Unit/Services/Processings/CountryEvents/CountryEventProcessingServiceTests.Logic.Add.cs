// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldAddCountryEventAsync()
        {
            // given
            CountryEvent randomCountryEvent = CreateRandomCountryEvent();
            CountryEvent inputCountryEvent = randomCountryEvent;
            CountryEvent addedCountryEvent = inputCountryEvent;
            CountryEvent expectedCountryEvent = addedCountryEvent.DeepClone();

            this.countryEventServiceMock.Setup(service =>
                service.AddCountryEventAsync(inputCountryEvent))
                    .ReturnsAsync(addedCountryEvent);

            // when
            CountryEvent actualCountryEvent =
                await this.countryEventProcessingService.AddCountryEventAsync(inputCountryEvent);

            // then
            actualCountryEvent.Should().BeEquivalentTo(expectedCountryEvent);

            this.countryEventServiceMock.Verify(service => 
                service.AddCountryEventAsync(inputCountryEvent),
                    Times.Once);

            this.countryEventServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
