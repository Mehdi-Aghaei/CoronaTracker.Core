// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class CountryValidationException : Xeption
    {
        public CountryValidationException(Xeption innerException)
            : base(message: "Country validation errors occurred, please fix the errors and try again.", innerException)
        { }
    }
}
