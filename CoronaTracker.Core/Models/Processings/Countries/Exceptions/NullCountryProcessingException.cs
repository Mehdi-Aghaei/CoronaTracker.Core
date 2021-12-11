// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class NullCountryProcessingException : Xeption
    {
        public NullCountryProcessingException()
            : base(message: "Country is null.") { }
    }
}
