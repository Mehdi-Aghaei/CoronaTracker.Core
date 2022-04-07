// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;

namespace CoronaTracker.Core.Models.ExternalCountryEvents
{
    public class ExternalCountryEvent
    {
        public Guid TrustedSourceId { get; set; }
        public ExternalCountry ExternalCountry { get; set; }
    }
}
