// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace CoronaTracker.Core.Models.Foundations.Countries.Exceptions
{
    public class NotFoundCountryException : Xeption
    {
        public NotFoundCountryException(Guid countryId)
            : base(message: $"Couldn't find country with id: {countryId}.")
        { }
    }
}
