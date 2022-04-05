// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Exceptions
{
    public class FailedExternalCountryOrchestrationServiceException : Xeption
    {
        public FailedExternalCountryOrchestrationServiceException(Exception innerException)
            : base(message: "failed External country error occurred, please try again.",
                  innerException)
        { }
    }
}
