// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Newtonsoft.Json;

namespace CoronaTracker.Core.Models.ExternalCountries
{
    public class CountryInfo
    {
        [JsonProperty(PropertyName = "_id")]
        public int? Id { get; set; }

        [JsonProperty("iso3")]
        public string Iso3 { get; set; }
    }
}
