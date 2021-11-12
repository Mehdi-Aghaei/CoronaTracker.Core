using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountries.Exceptions
{
    public class ExternalCountryDependencyException : Xeption
    {
        public ExternalCountryDependencyException(Xeption innerException)
            : base(message: "External country dependency error occurred, contact support.", innerException)
        { }
    }
}
