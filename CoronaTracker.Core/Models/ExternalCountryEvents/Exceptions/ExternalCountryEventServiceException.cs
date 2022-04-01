// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventServiceException : Xeption
    {
        public ExternalCountryEventServiceException(Xeption innerException)
            : base(message: "External country event service error occurred, please contact support", innerException)
        { }
    }
}
