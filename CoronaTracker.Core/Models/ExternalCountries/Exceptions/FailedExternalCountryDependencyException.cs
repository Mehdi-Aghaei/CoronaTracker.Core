using System;
using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountries.Exceptions
{
    public class FailedExternalCountryDependencyException : Xeption
    {
        public FailedExternalCountryDependencyException(Exception innerException)
             : base(message: "Failed external country dependency error occurred, please contact support.", innerException)
        {}
    }
}
