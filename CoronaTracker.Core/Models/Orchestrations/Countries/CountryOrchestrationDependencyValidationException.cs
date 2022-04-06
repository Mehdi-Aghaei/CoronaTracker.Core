// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Orchestrations.Countries
{
    public class CountryOrchestrationDependencyValidationException : Xeption
    {
        public CountryOrchestrationDependencyValidationException(Xeption innerException)
             : base(message: "Country dependency validation error occurred, please fix the errors and try again.",
                  innerException)
        { }
    }
}
