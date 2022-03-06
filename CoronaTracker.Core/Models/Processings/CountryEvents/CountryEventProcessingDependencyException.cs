// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.CountryEvents
{
    public class CountryEventProcessingDependencyException : Xeption
    {
        public CountryEventProcessingDependencyException(Xeption innerException)
            : base(message: "Country event dependency exception occurred, Please try again.", innerException)
        { }
    }
}
