// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.CountryEvents
{
    public class NullCountryEventProcessingException : Xeption
    {
        public NullCountryEventProcessingException()
            : base(message: "Country event is null.")
        { }
    }
}
