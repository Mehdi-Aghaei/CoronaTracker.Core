// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions
{
    public class NullExternalCountryEventException : Xeption
    {
        public NullExternalCountryEventException()
        : base(message: "External country event is null.")
        { }
    }
}
