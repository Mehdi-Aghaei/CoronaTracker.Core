// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.CountryEvents.Exceptions
{
    public class NullCountryEventException : Xeption
    {
        public NullCountryEventException()
        : base(message: "Country event is null.")
        { }
    }
}
