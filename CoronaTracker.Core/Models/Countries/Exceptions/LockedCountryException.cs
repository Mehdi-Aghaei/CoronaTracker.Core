using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class LockedCountryException : Xeption
    {
        public LockedCountryException(Exception innerException)
            : base(message: " Locked country record exception, please try again later", innerException)
        { }
    }
}
