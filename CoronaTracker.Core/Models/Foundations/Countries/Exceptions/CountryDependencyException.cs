// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Countries.Exceptions
{
    public class CountryDependencyException : Xeption
    {
        public CountryDependencyException(Xeption innerException)
            : base(message: "Country dependency error occurred, contact support.", innerException)
        { }
    }
}
