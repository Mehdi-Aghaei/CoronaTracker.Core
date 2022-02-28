// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.CountryEvents.Exceptions
{
    public class FailedCountryEventServiceException : Xeption
    {
        public FailedCountryEventServiceException(Exception innerException)
            : base(message: "Failed country event service error occured, please contact support", innerException)
        { }
    }
}
