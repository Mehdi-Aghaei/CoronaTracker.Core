// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Orchestrations.ExternalCountryEvents
{
    public partial class ExternalCountryEventOrchestrationServiceTests
    {
        [Fact]
        public void ShouldAddExternalCountryToQueue()
        {
            // given
            List<ExternalCountry> randomExternalCountries =
                CreateRandomExternalCountries();

            List<ExternalCountry> returningExternalCountries =
                randomExternalCountries;

            var mockSequence = new MockSequence();

            this.externalCountryProcessingServiceMock.InSequence(mockSequence)
                .Setup(service => service.RetrieveAllExternalCountriesAsync())
                    .ReturnsAsync(returningExternalCountries);

            foreach (var externalCountry in returningExternalCountries)
            {
                var expectedExternalCountryEvent = new ExternalCountryEvent
                {
                    ExternalCountry = externalCountry
                };
                this.externalCountryEventProcessingServiceMock.InSequence(mockSequence)
                    .Setup(service => service.AddExternalCountryEventAsync(expectedExternalCountryEvent));
            }

            // when
            this.externalCountryEventOrchestrationService.AddExternalCountryToQueueAsync();

            // then
            this.externalCountryProcessingServiceMock.Verify(service =>
                service.RetrieveAllExternalCountriesAsync(),
                    Times.AtLeastOnce);

            this.externalCountryEventProcessingServiceMock.Verify(service =>
                service.AddExternalCountryEventAsync(It.IsAny<ExternalCountryEvent>()),
                    Times.AtLeastOnce);

            this.externalCountryProcessingServiceMock.VerifyNoOtherCalls();
            this.externalCountryEventProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
