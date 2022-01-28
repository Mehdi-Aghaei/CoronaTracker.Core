﻿// ---------------------------------------------------------------
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
            : base("Failed external country processing service occurred, please contact support", innerException)
        { }
    }
}
