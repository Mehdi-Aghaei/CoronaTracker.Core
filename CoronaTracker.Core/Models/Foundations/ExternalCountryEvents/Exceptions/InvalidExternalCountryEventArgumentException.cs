// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.ExternalCountryEvents.Exceptions
{
    public class InvalidExternalCountryEventArgumentException : Xeption
    {
        public InvalidExternalCountryEventArgumentException(Exception innerException)
            : base(message: "Invalid external country event argument error occurred.", innerException)
        { }
    }
}
