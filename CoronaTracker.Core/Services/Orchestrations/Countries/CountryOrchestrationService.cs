// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.DateTimes;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Foundations.ExternalCountries;
using CoronaTracker.Core.Services.Processings.Countries;
using CoronaTracker.Core.Services.Processings.ExternalCountries;

namespace CoronaTracker.Core.Services.Orchestrations.Countries
{
    public partial class CountryOrchestrationService : ICountryOrchestrationService
    {
        private readonly IExternalCountryProcessingService externalCountryProcessingService;
        private readonly ICountryProcessingService countryProcessingService;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public CountryOrchestrationService(
            IExternalCountryProcessingService externalCountryProcessingService,
            ICountryProcessingService countryProcessingService,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.externalCountryProcessingService = externalCountryProcessingService;
            this.countryProcessingService = countryProcessingService;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<IQueryable<Country>> RetrieveAllCountriesAsync() =>
        TryCatch(async () =>
        {
            if (this.countryProcessingService.RetrieveAllCountries() is null)
            {
                List<ExternalCountry> allExternalCountries =
                    await this.externalCountryProcessingService
                        .RetrieveAllExternalCountriesAsync();
                if (allExternalCountries.Count < 230)
                {
                    foreach (var externalCountry in allExternalCountries)
                    {
                        await UpsertCountryAsync(externalCountry);
                    }
                }
            }

            return this.countryProcessingService.RetrieveAllCountries();
        });

        private async ValueTask<Country> UpsertCountryAsync(ExternalCountry persistedExternalCountry)
        {
            if (persistedExternalCountry.CountryName is "Diamond Princess")
            {
                persistedExternalCountry.CountryInfo.Iso3 = "Not specified";
                persistedExternalCountry.Continent = "Not specified";
            }
            if (persistedExternalCountry.CountryName is "MS Zaandam")
            {
                persistedExternalCountry.CountryInfo.Iso3 = "Not specified";
                persistedExternalCountry.Continent = "Not specified";
            }

            Country country = MapToCountry(persistedExternalCountry);

            bool isCountryChanged = this.countryProcessingService.VerifyCountryChanged(country);

            return isCountryChanged
                ? await this.countryProcessingService.UpsertCountryAsync(country)
                : country;
        }

        private Country MapToCountry(ExternalCountry persistedExternalCountry)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            var country = new Country
            {
                Id = Guid.NewGuid(),
                Name = persistedExternalCountry.CountryName,
                Iso3 = persistedExternalCountry.CountryInfo.Iso3,
                Continent = persistedExternalCountry.Continent,
                Cases = persistedExternalCountry.Cases,
                TodayCases = persistedExternalCountry.TodayCases,
                Deaths = persistedExternalCountry.Deaths,
                TodayDeaths = persistedExternalCountry.TodayDeaths,
                Recovered = persistedExternalCountry.Recovered,
                TodayRecovered = persistedExternalCountry.TodayRecovered,
                Population = persistedExternalCountry.Population,
                CasesPerOneMillion = persistedExternalCountry.CasesPerOneMillion,
                DeathsPerOneMillion = persistedExternalCountry.DeathsPerOneMillion,
                RecoveredPerOneMillion = persistedExternalCountry.RecoveredPerOneMillion,
                CriticalPerOneMillion = persistedExternalCountry.CriticalPerOneMillion,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime
            };
            return country;
        }
    }
}
