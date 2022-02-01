// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountries.Exceptions
{
    public class FailedExternalCountryProcessingServiceException : Xeption
    {
        public FailedExternalCountryProcessingServiceException(Exception innerException)
            : base(message:"Failed external country service occurred, please contact support", innerException)
        { }
    }
}
