using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class NotFoundCountryException : Xeption
    {
        public NotFoundCountryException(Guid countryId)
            : base(message: $"Couldn't find country with id: {countryId}.")
        { }
    }
}
