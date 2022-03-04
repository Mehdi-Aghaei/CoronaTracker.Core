// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.CountryEvents.Exceptions
{
    public class CountryEventServiceException : Xeption
    {
        public CountryEventServiceException(Xeption innerException)
            : base(message: "Country event service error occured, please contact support", innerException)
        { }
    }
}
