// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountries.Exceptions
{
    public class ExternalCountryServiceException : Xeption
    {
        public ExternalCountryServiceException(Xeption innerException)
            : base(message: "External country service error occurred, contact support.", innerException)
        { }
    }
}
