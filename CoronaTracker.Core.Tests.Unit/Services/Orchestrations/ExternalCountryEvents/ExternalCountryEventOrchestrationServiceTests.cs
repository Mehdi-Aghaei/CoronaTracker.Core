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
using CoronaTracker.Core.Services.Foundations.ExternalCountries;
using CoronaTracker.Core.Services.Orchestrations.ExternalCountryEvents;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Moq;
using Tynamix.ObjectFiller;

namespace CoronaTracker.Core.Tests.Unit.Services.Orchestrations.ExternalCountryEvents
{
    public partial class ExternalCountryEventOrchestrationServiceTests
    {
        private readonly Mock<IExternalCountryProcessingService> externalCountryProcessingServiceMock;
        private readonly Mock<IExternalCountryEventProcessingService> externalCountryEventProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IExternalCountryEventOrchestrationService externalCountryEventOrchestrationService;

        public ExternalCountryEventOrchestrationServiceTests()
        {
            this.externalCountryProcessingServiceMock = new Mock<IExternalCountryProcessingService>();
            this.externalCountryEventProcessingServiceMock = new Mock<IExternalCountryEventProcessingService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            
            this.externalCountryEventOrchestrationService = new ExternalCountryEventOrchestrationService(
                externalCountryProcessingService:this.externalCountryProcessingServiceMock.Object,
                externalCountryEventProcessingService:this.externalCountryEventProcessingServiceMock.Object,
                loggingBroker:this.loggingBrokerMock.Object);
        }
        
        
        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<ExternalCountry> CreateRandomExternalCountries() =>
            CreateExternalCountryFiller().Create(count:GetRandomNumber()).ToList();

        private static Filler<ExternalCountry> CreateExternalCountryFiller() =>
            new Filler<ExternalCountry>();
    }
}
