using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class CountryDependencyException : Xeption
    {
        public CountryDependencyException(Xeption innerException)
            : base(message: "Country dependency error occurred, contact support.", innerException)
        { }
    }
}
