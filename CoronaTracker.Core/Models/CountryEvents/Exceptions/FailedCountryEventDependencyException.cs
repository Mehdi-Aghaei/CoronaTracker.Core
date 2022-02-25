// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.CountryEvents.Exceptions
{
    public class FailedCountryEventDependencyException : Xeption
    {
        public FailedCountryEventDependencyException(Exception innerException)
            : base(message: "Country event dependency error occured, please contact support", innerException)
        { }
    }
}
