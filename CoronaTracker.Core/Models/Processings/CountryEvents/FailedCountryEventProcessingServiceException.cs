﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Processings.CountryEvents
{
    public class FailedCountryEventProcessingServiceException : Xeption
    {
        public FailedCountryEventProcessingServiceException(Exception innerException)
            : base(message: "Failed country event service occurred, please contact support.", innerException)
        { }
    }
}