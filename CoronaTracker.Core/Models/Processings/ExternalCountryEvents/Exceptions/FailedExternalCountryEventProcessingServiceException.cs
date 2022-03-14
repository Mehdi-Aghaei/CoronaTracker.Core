// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions
{
    public class FailedExternalCountryEventProcessingServiceException : Xeption
    {
        public FailedExternalCountryEventProcessingServiceException(Exception innerException)
            : base(message: "Failed country event service occurred, please contact support.", innerException)
        { }
    }
}
