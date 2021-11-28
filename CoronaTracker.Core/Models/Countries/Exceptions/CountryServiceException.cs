// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class CountryServiceException : Xeption
    {
        public CountryServiceException(Xeption innerException)
            : base(message: "Country Service error occurred, contact support", innerException)
        { }
    }
}
