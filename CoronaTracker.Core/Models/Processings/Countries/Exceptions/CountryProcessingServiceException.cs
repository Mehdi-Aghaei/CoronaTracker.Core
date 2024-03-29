﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace CoronaTracker.Core.Models.Processings.Countries.Exceptions
{
    public class CountryProcessingServiceException : Xeption
    {
        public CountryProcessingServiceException(Xeption innerException)
            : base(message: "Country service error occurred, please contact support", innerException)
        { }
    }
}
