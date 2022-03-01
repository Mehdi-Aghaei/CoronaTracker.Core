// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.CountryEvents.Exceptions
{
    public class CountryEventValidationException : Xeption
    {
        public CountryEventValidationException(Xeption innerException)
            :base(message:"Country event validation exception occurred, Please try again.",innerException)
        {

        }
    }
}
