using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class CountryDependencyValidationException : Xeption
    {
        public CountryDependencyValidationException(Xeption innerException)
            : base(message: "Country dependency validation occurred,fix the errors and try again.", innerException)
        { }
    }
}
