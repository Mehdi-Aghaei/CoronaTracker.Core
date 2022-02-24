// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Services.Foundations.CountryEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Tynamix.ObjectFiller;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.CountryEvents;

public partial class CountryEventServiceTests
{
    private readonly Mock<IQueueBroker> queueBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly ICompareLogic compareLogic;
    private readonly ICountryEventService countryEventService;

    public CountryEventServiceTests()
    {
        this.queueBrokerMock = new Mock<IQueueBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();
        this.compareLogic = new CompareLogic();

        this.countryEventService = new CountryEventService(
            queueBroker: this.queueBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);

    }

    private static CountryEvent CreateRandomCountryEvent() =>
        CreateCountryEventFiller(dates: GetRandomDateTime()).Create();

    private static DateTimeOffset GetRandomDateTime() =>
        new DateTimeRange(earliestDate: new DateTime()).GetValue();

    private Expression<Func<Message, bool>> SameMessageAs(Message expectedMessage)
    {
        return actualMessage =>
            this.compareLogic.Compare(expectedMessage, actualMessage).AreEqual;
            
    }

    private static Filler<CountryEvent> CreateCountryEventFiller(DateTimeOffset dates)
    {
        var filler = new Filler<CountryEvent>();

        filler.Setup().OnType<DateTimeOffset>().Use(dates);

        return filler;
    }
}