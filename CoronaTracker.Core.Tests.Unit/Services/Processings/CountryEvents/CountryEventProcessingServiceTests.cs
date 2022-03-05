// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Services.Foundations.CountryEvents;
using CoronaTracker.Core.Services.Processings.CountryEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingServiceTests
    {
        private readonly Mock<ICountryEventService> countryEventServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICountryEventProcessingService countryEventProcessingService;

        public CountryEventProcessingServiceTests()
        {
            this.countryEventServiceMock = new Mock<ICountryEventService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.countryEventProcessingService = new CountryEventProcessingService(
                countryEventService: countryEventServiceMock.Object,
                loggingBroker: loggingBrokerMock.Object);

        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static CountryEvent CreateRandomCountryEvent() =>
            CreateCountryEventFiller().Create();

        private static Filler<CountryEvent> CreateCountryEventFiller()
        {
            var filler = new Filler<CountryEvent>();

            filler.Setup().OnType<DateTimeOffset>()
                .Use(GetRandomDateTime());
        
            return filler;
        }
    }
}
