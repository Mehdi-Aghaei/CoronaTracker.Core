// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions
{
    public class InvalidExternalCountryEventException : Xeption
    {
        public InvalidExternalCountryEventException()
            : base(message: "External country event is invalid")
        { }
    }
}
