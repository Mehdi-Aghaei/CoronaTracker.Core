// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Countries.Exceptions
{
    public class CountryOrchestrationDependencyException : Xeption
    {
        public CountryOrchestrationDependencyException(Xeption innerException)
             : base(message: "Country dependency error occurred, please fix the errors and try again.",
                  innerException)
        { }
    }
}
