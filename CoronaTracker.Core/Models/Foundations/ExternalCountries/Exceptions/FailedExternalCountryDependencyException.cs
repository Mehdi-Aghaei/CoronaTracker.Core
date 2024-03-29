﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountries.Exceptions
{
    public class FailedExternalCountryDependencyException : Xeption
    {
        public FailedExternalCountryDependencyException(Exception innerException)
            : base(message: "Failed external country dependency error occurred, please contact support.", innerException)
        { }
    }
}
