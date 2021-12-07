// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class CountryProcessingValidationException : Xeption
    {
        public CountryProcessingValidationException(Xeption innerException)
            : base(message: "Country validation error occurred, please try again.",
                  innerException)
        { }
    }
}
