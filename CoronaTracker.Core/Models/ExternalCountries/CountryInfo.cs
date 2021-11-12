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
