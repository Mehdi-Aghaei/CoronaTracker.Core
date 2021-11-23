using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class AlreadyExistsCountryException : Xeption
    {
        public AlreadyExistsCountryException(Exception innerException)
             : base(message: "Country with the same id already exists.", innerException)
        { }
    }
}
