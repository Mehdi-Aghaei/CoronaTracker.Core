// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountries.Exceptions
{
    public class ExternalCountryDependencyException : Xeption
    {
        public ExternalCountryDependencyException(Xeption innerException)
            : base(message: "External country dependency error occurred, contact support.", innerException)
        { }
    }
}
