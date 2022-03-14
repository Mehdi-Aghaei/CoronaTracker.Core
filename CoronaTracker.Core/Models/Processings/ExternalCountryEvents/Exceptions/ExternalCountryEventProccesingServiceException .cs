// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventProccesingServiceException : Xeption
    {
        public ExternalCountryEventProccesingServiceException(Xeption innerException)
            : base(message: "Country event service error occurred, please contact support", innerException)
        { }
    }
}
