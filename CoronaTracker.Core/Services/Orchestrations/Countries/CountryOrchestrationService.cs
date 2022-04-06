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
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.ExternalCountries;
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

        public ValueTask<IQueryable<Country>> ProcessAllExternalCountriesAsync() =>
        TryCatch(async () =>
        {
            List<ExternalCountry> allExternalCountries =
                await this.externalCountryProcessingService
                    .RetrieveAllExternalCountriesAsync();

            foreach (var externalCountry in allExternalCountries)
            {
                await MapToCountryAndUpsertAsync(externalCountry);
            }

            return this.countryProcessingService.RetrieveAllCountries();
        });

        private async ValueTask<Country> MapToCountryAndUpsertAsync(ExternalCountry persistedExternalCountry)
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

            bool isCountryChanged = this.countryProcessingService.VerifyCountryChanged(country);

            return isCountryChanged
                ? await this.countryProcessingService.UpsertCountryAsync(country)
                : country;
        }
    }
}
