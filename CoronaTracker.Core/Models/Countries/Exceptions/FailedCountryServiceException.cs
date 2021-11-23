using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class FailedCountryServiceException : Xeption
    {
        public FailedCountryServiceException(Exception innerException)
            : base(message: "Failed country service occurred, please contact support", innerException)
        { }
    }
}
