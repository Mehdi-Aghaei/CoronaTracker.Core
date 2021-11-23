using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class InvalidCountryException : Xeption
    {
        public InvalidCountryException()
            : base(message: "Invalid country. Please correct the errors and try again.")
        { }
    }
}
