// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTrackerHungary.Web.Api.Models.Countries.Exceptions
{
    public class ExternalCountryServiceException : Xeption
    {
        public ExternalCountryServiceException(Xeption innerException)
            : base(message: "External country service error occurred, contact support.", innerException)
        { }
    }
}
