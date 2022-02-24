// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Models.Countries;

namespace CoronaTracker.Core.Models.CountryEvents
{
    public class CountryEvent
    {
        public Guid TrustedId { get; set; }
        public Country Country { get; set; }
    }
}
