// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class FailedCountryProcessingSeviceException : Xeption
    {
        public FailedCountryProcessingSeviceException(Exception innerException)
            : base("Failed country processing service occurred, please contact support", innerException)
        { }
    }
}
