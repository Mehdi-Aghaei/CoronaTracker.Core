// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions
{
    public class FailedExternalCountryEventServiceException : Xeption
    {
        public FailedExternalCountryEventServiceException(Exception innerException)
            : base(message: "Failed external country event service error occurred, please contact support", innerException)
        { }
    }
}
