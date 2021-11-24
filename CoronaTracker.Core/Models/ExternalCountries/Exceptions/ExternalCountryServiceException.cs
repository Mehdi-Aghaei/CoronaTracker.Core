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
