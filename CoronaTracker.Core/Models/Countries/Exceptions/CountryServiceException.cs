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
