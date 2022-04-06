// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Countries
{
    public class FailedCountryOrchestrationServiceException : Xeption
    {
        public FailedCountryOrchestrationServiceException(Exception innerException)
            : base(message: "Failed country service error occurred, contact support.",
                  innerException)
        { }
    }
}
