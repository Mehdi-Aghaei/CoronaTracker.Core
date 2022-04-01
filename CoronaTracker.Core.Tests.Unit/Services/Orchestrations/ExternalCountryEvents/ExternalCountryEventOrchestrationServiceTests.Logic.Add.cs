// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Services.Foundations.ExternalCountries;
using CoronaTracker.Core.Services.Orchestrations.ExternalCountryEvents;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Force.DeepCloner;
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
