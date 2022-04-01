// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using CoronaTracker.Core.Brokers.Configurations;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Brokers.Queues;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Services.Foundations.ExternalCountryEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;
using Messaging = Microsoft.ServiceBus.Messaging;
using MessagingEntityDisabledException = Microsoft.Azure.ServiceBus.MessagingEntityDisabledException;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.ExternalCountryEvents;

public partial class ExternalCountryEventServiceTests
{
    private readonly Mock<IQueueBroker> queueBrokerMock;
    private readonly Mock<IConfigurationBroker> configuratinBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly ICompareLogic compareLogic;
    private readonly IExternalCountryEventService externalCountryEventService;

    public ExternalCountryEventServiceTests()
    {
        this.queueBrokerMock = new Mock<IQueueBroker>();
        this.configuratinBrokerMock = new Mock<IConfigurationBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();
        this.compareLogic = new CompareLogic();

        this.externalCountryEventService = new ExternalCountryEventService(
            queueBroker: this.queueBrokerMock.Object,
            configurationBroker: this.configuratinBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);

    }

    public static TheoryData MessageQueueExceptions()
    {
        string message = GetRandomString();

        return new TheoryData<Exception>
        {
            new MessagingEntityNotFoundException(message),
            new MessagingEntityDisabledException(message),
            new UnauthorizedAccessException()
        };
    }
    
    public static TheoryData MessageQueueDependencyExceptions()
    {
        string message = GetRandomString();

        return new TheoryData<Exception>{
            new InvalidOperationException(),
            new Messaging.MessagingCommunicationException(communicationPath: message),
            new ServerBusyException(message: message),
        };
    }
    
    private static Message CreateExternalCountryMessage(ExternalCountry externalCountry)
    {
        string serializedExternalCountry = JsonSerializer.Serialize(externalCountry);
        byte[] externalCountryBody = Encoding.UTF8.GetBytes(serializedExternalCountry);

        return new Message
        {
            Body = externalCountryBody
        };
    }

    private static ExternalCountryEvent CreateRandomExternalCountryEvent() =>
        CreateExternalCountryEventFiller(dates: GetRandomDateTime()).Create();

    private static ExternalCountry CreateRandomExternalCountry() =>
        CreateExternalCountryFiller(dates: GetRandomDateTime()).Create();

    private static DateTimeOffset GetRandomDateTime() =>
        new DateTimeRange(earliestDate: new DateTime()).GetValue();

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private Expression<Func<Message, bool>> SameMessageAs(Message expectedMessage)
    {
        return actualMessage =>
            this.compareLogic.Compare(expectedMessage, actualMessage).AreEqual;

    }

    private Expression<Func<ExternalCountry, bool>> SameExternalCountryAs(ExternalCountry expectedExternalCountry)
    {
        return actualExternalCountry =>
            this.compareLogic.Compare(expectedExternalCountry, actualExternalCountry).AreEqual;
    }


    private Expression<Func<ExternalCountry, bool>> SameExternalCountryAs(ExternalCountry expectedExternalCountry)
    {
        return actualExternalCountry =>
            this.compareLogic.Compare(expectedExternalCountry, actualExternalCountry).AreEqual;
    }

    private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
    {
        return actualException =>
            actualException.Message == expectedException.Message
            && actualException.InnerException.Message == expectedException.InnerException.Message
            && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
    }

    private static Filler<ExternalCountryEvent> CreateExternalCountryEventFiller(DateTimeOffset dates)
    {
        var filler = new Filler<ExternalCountryEvent>();

        filler.Setup().OnType<DateTimeOffset>().Use(dates);

        return filler;
    }
    private static Filler<ExternalCountry> CreateExternalCountryFiller(DateTimeOffset dates)
    {
        var filler = new Filler<ExternalCountry>();

        filler.Setup().OnType<DateTimeOffset>().Use(dates);

        return filler;
    }
}