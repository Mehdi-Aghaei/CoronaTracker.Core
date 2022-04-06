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
using CoronaTracker.Core.Models.Orchestrations.Exceptions;
using CoronaTracker.Core.Services.Foundations.Countries;
using CoronaTracker.Core.Services.Processings.Countries;
using CoronaTracker.Core.Services.Processings.ExternalCountries;
using Xeptions;

namespace CoronaTracker.Core.Services.Orchestrations.Countries
{
    public partial class CountryOrchestrationService : ICountryOrchestrationService
    {
        private readonly IExternalCountryProcessingService externalCountryProcessingService;
        private readonly ICountryProcessingService countryProcessingService;
        private readonly ICountryService countryService;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public CountryOrchestrationService(
            IExternalCountryProcessingService externalCountryProcessingService,
            ICountryProcessingService countryProcessingService,
            ICountryService countryService,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.externalCountryProcessingService = externalCountryProcessingService;
            this.countryProcessingService = countryProcessingService;
            this.countryService = countryService;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<IQueryable<Country>> ProcessAllExternalCountriesAsync()
        {
            try
            {
                List<ExternalCountry> allExternalCountries =
                await this.externalCountryProcessingService
                    .RetrieveAllExternalCountriesAsync();

                foreach (var externalCountry in allExternalCountries)
                {
                    await MapToCountryAndUpsertAsync(externalCountry);
                }

                return this.countryService.RetrieveAllCountries();
            }
            catch (Exception exception)
            {
                var failedExternalCountryOrchestrationServiceException =
                    new FailedExternalCountryOrchestrationServiceException(exception);

                throw CreateAndLogServiceException(failedExternalCountryOrchestrationServiceException);
            }
        }

        private ExternalCountryOrchestrationServiceException CreateAndLogServiceException(Xeption exception)
        {
            var externalCountryOrchestrationServiceException =
               new ExternalCountryOrchestrationServiceException(exception);

            this.loggingBroker.LogError(externalCountryOrchestrationServiceException);

            return externalCountryOrchestrationServiceException;
        }

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
