using System;
using System.ComponentModel.DataAnnotations;

namespace CoronaTracker.Core.Models.Countries
{
    public class Country
    {
        [Key]
        public string CountryName { get; set; }
        public string Iso3 { get; set; }
        public string Continent { get; set; }
        public int Cases { get; set; }
        public int TodayCases { get; set; }
        public int Deaths { get; set; }
        public int TodayDeaths { get; set; }
        public int Recovered { get; set; }
        public int TodayRecovered { get; set; }
        public int Population { get; set; }
        public int CasesPerOneMillion { get; set; }
        public int DeathsPerOneMillion { get; set; }
        public float RecoveredPerOneMillion { get; set; }
        public float criticalPerOneMillion { get; set; }
    }
}
