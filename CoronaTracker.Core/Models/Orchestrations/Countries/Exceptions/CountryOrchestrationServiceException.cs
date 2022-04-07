// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Countries.Exceptions
{
    public class CountryOrchestrationServiceException : Xeption
    {
        public CountryOrchestrationServiceException(Xeption innerException)
            : base(message: "Country service error occurred, please fix the errors and try again.",
                  innerException)
        { }
    }
}
