// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class InvalidCountryProcessingException : Xeption
    {
        public InvalidCountryProcessingException()
            : base(message: "Invalid country, Please correct the errors and try again.")
        { }
    }
}
