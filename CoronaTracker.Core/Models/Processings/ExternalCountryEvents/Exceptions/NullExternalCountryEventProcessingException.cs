// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions
{
    public class NullExternalCountryEventProcessingException : Xeption
    {
        public NullExternalCountryEventProcessingException()
            : base(message: "Country event is null.")
        { }
    }
}
