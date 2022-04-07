// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.Countries.Exceptions
{
    public class NullCountryException : Xeption
    {
        public NullCountryException()
            : base(message: "Country is null.")
        { }
    }
}
