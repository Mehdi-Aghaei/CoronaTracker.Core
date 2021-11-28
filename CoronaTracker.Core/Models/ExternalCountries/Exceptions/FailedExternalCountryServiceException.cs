// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTrackerHungary.Web.Api.Models.Countries.Exceptions
{
    public class FailedExternalCountryServiceException : Xeption
    {
        public FailedExternalCountryServiceException(Exception innerException)
            : base(message: "Failed external country error occurred, please contact support", innerException)
        { }
    }
}
