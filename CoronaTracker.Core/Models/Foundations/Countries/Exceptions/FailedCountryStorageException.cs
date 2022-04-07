// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.Countries.Exceptions
{
    public class FailedCountryStorageException : Xeption
    {
        public FailedCountryStorageException(Exception innerException)
            : base(message: "Failed country storage error occurred, contact support.", innerException)
        { }
    }
}
