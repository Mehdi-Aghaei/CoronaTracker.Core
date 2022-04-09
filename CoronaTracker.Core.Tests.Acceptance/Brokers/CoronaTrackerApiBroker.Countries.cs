﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;

namespace CoronaTracker.Core.Tests.Acceptance.Brokers
{
    public partial class CoronaTrackerApiBroker
    {
        private const string CountriesRelativeUrl = "api/countries";
        public async ValueTask<List<Country>> GetAllCountriesAsync() =>
            await this.apiFactoryClient.GetContentAsync<List<Country>>($"{CountriesRelativeUrl}/");
    }
}
