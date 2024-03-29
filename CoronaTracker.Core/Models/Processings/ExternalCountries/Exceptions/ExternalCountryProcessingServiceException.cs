﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.ExternalCountries.Exceptions
{
    public class ExternalCountryProcessingServiceException : Xeption
    {
        public ExternalCountryProcessingServiceException(Xeption innerException)
            : base(message: "External country service error occurred, please contact support", innerException)
        { }
    }
}
