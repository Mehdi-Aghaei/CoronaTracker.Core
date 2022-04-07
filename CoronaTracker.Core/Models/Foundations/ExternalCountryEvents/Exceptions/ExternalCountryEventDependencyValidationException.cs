// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventDependencyValidationException : Xeption
    {
        public ExternalCountryEventDependencyValidationException(Xeption innerException)
            : base(message: "External country event dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
