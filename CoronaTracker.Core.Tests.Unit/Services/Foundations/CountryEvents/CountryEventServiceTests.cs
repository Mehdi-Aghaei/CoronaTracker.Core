// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Reflection.Emit;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Services.Foundations.CountryEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.CountryEvents;

public partial class CountryEventServiceTests
{
    private Mock<IQueueBroker> queueBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly ICountryEventService countryEventService;

    public CountryEventServiceTests()
    {
        this.queueBrokerMock = new Mock<IQueueBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();
        this.countryEventService = new CountryEventService(
            queueBroker: this.queueBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);

    }

    private static CountryEvent CreateRandomCountryEvent() =>
        CreateCountryEventFiller().Create();


    private static Filler<CountryEvent> CreateCountryEventFiller() =>
        new Filler<CountryEvent>();
}