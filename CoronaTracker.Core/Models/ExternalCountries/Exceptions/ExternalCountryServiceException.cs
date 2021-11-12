using System;
using Xeptions;

namespace CoronaTrackerHungary.Web.Api.Models.Countries.Exceptions
{
    public class ExternalCountryServiceException : Xeption
    {
        public ExternalCountryServiceException(Exception innerException)
            : base(message: "External country service error occurred, contact support.", innerException)
        { }
    }
}
