// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions
{
    public class ExternalCountryEventValidationException : Xeption
    {
        public ExternalCountryEventValidationException(Xeption innerException)
            : base(message: "External country event validation exception occurred, Please try again.", innerException)
        {

        }
    }
}
