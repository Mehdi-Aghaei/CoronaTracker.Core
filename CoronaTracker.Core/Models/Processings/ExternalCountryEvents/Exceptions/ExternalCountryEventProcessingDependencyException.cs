// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventProcessingDependencyException : Xeption
    {
        public ExternalCountryEventProcessingDependencyException(Xeption innerException)
            : base(message: "Country event dependency exception occurred, Please try again.", innerException)
        { }
    }
}
