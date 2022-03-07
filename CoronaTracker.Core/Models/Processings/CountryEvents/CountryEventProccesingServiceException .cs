// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.CountryEvents
{
    public class CountryEventProccesingServiceException : Xeption
    {
        public CountryEventProccesingServiceException(Xeption innerException)
            : base(message: "Country event service error occurred, please contact support", innerException)
        { }
    }
}
