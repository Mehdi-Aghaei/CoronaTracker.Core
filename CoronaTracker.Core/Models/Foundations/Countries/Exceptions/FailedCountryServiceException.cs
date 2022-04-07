// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.Countries.Exceptions
{
    public class FailedCountryServiceException : Xeption
    {
        public FailedCountryServiceException(Exception innerException)
            : base(message: "Failed country service error occurred, please contact support", innerException)
        { }
    }
}
