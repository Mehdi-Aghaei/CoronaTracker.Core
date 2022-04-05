// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Exceptions
{
    public class ExternalCountryOrchestrationServiceException : Xeption
    {
        public ExternalCountryOrchestrationServiceException(Xeption innerException)
            : base(message: "External country service error occurred, please fix the errors and try again.",
                  innerException)
        { }
    }
}
