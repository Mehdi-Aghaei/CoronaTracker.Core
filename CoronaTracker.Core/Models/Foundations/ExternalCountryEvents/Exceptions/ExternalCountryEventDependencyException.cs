// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventDependencyException : Xeption
    {
        public ExternalCountryEventDependencyException(Xeption innerException)
            : base(message: "External country event dependency error occurred, please contact support", innerException)
        { }
    }
}
