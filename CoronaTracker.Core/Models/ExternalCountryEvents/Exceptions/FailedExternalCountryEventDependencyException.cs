// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions
{
    public class FailedExternalCountryEventDependencyException : Xeption
    {
        public FailedExternalCountryEventDependencyException(Exception innerException)
            : base(message: "External country event dependency error occurred, please contact support", innerException)
        { }
    }
}
