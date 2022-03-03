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
using Messaging = Microsoft.ServiceBus.Messaging;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;
using Microsoft.ServiceBus.Messaging;
using Xeptions;
using MessagingEntityDisabledException = Microsoft.Azure.ServiceBus.MessagingEntityDisabledException;
using CoronaTracker.Core.Brokers.Configurations;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.CountryEvents;

public partial class CountryEventServiceTests
{
    private readonly Mock<IQueueBroker> queueBrokerMock;
    private readonly Mock<IConfigurationBroker> configuratinBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly ICompareLogic compareLogic;
    private readonly ICountryEventService countryEventService;

    public CountryEventServiceTests()
    {
        this.queueBrokerMock = new Mock<IQueueBroker>();
        this.configuratinBrokerMock = new Mock<IConfigurationBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();
        this.compareLogic = new CompareLogic();

        this.countryEventService = new CountryEventService(
            queueBroker: this.queueBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);

    }

    public static TheoryData DependencyMessageQueueExceptions()
    {
        string randomString = GetRandomString();
        string exceptionMessage = randomString;

        return new TheoryData<Exception>{
            new MessagingException(message: exceptionMessage),
            new Messaging.ServerBusyException(message: exceptionMessage),
            new MessagingCommunicationException(communicationPath: randomString)
        };
    }

    public static TheoryData CriticalDependencyMessageQueueExceptions()
    {
        string randomString = GetRandomString();
        string exceptionMessage = randomString;

        return new TheoryData<Exception>
        {
            new UnauthorizedAccessException(),
            new MessagingEntityDisabledException(exceptionMessage)
        };
    }

    private static CountryEvent CreateRandomCountryEvent() =>
        CreateCountryEventFiller(dates: GetRandomDateTime()).Create();

    private static DateTimeOffset GetRandomDateTime() =>
        new DateTimeRange(earliestDate: new DateTime()).GetValue();

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private Expression<Func<Message, bool>> SameMessageAs(Message expectedMessage)
    {
        return actualMessage =>
            this.compareLogic.Compare(expectedMessage, actualMessage).AreEqual;
            
    }

    private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
    {
        return actualException =>
            actualException.Message == expectedException.Message
            && actualException.InnerException.Message == expectedException.InnerException.Message
            && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
    }

    private static Filler<CountryEvent> CreateCountryEventFiller(DateTimeOffset dates)
    {
        var filler = new Filler<CountryEvent>();

        filler.Setup().OnType<DateTimeOffset>().Use(dates);

        return filler;
    }
}