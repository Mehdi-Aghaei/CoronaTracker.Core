// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.CountryEvents
{
    public class CountryEventProcessingValidationException : Xeption
    {
        public CountryEventProcessingValidationException(Xeption innerException)
            : base(message: "Country event validation exception occurred, Please try again.", innerException)
        { }
    }
}
