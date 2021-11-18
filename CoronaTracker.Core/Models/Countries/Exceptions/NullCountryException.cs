using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class NullCountryException : Xeption
    {
        public NullCountryException()
            : base(message: "Country is null.")
        { }
    }
}
