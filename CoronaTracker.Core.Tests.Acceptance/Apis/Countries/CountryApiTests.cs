// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaTracker.Core.Tests.Acceptance.Brokers;
using Xunit;

namespace CoronaTracker.Core.Tests.Acceptance.Apis.Countries
{
    [Collection(nameof(ApiTestCollection))]
    public partial class CountryApiTests
    {
        private readonly CoronaTrackerApiBroker apiBroker;

        public CountryApiTests(CoronaTrackerApiBroker apiBroker) =>
            this.apiBroker = apiBroker;
    }
}
