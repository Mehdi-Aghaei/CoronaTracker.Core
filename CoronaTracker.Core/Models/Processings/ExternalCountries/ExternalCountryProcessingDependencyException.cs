// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountries
{
    public class ExternalCountryProcessingDependencyException : Xeption
    {
        public ExternalCountryProcessingDependencyException(Xeption innerException)
            : base(message: "External country dependency error occurred, please contact support.", innerException)
        { }
    }
}
