// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class CountryProcessingDependencyValidationException : Xeption
    {
        public CountryProcessingDependencyValidationException(Xeption innerException)
            : base(message: "Country dependency validation error occurred, please try again.",
                  innerException)
        { }
    }
}
