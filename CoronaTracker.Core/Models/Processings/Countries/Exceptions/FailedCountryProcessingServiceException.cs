// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class FailedCountryProcessingServiceException : Xeption
    {
        public FailedCountryProcessingServiceException(Exception innerException)
            : base(message: "Failed country service occurred, please contact support", innerException)
        { }
    }
}
