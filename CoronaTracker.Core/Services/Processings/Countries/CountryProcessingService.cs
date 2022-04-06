// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoronaTracker.Core.Brokers.Loggings;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Services.Foundations.Countries;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public partial class CountryProcessingService : ICountryProcessingService
    {
        private readonly ICountryService countryService;
        private readonly ILoggingBroker loggingBroker;

        public CountryProcessingService(
            ICountryService countryService,
            ILoggingBroker loggingBroker)
        {
            this.countryService = countryService;
            this.loggingBroker = loggingBroker;
        }
        public bool VerifyCountryChanged(Country country) =>
        TryCatch(() =>
        {
            ValidateCountryOnVerify(country);
            Country maybeCountry = RetrieveMatchingCountry(country);

            return maybeCountry switch
            {
                null => true,
                _ => IsCountryChanged(country, maybeCountry)
            };
        });

        public ValueTask<Country> UpsertCountryAsync(Country country) =>
        TryCatch(async () =>
        {
            if (country.Name is "Diamond Princess")
            {
                country.Iso3 = "Not specified";
                country.Continent = "Not specified";
            }
            if (country.Name is "MS Zaandam")
            {
                country.Iso3 = "Not specified";
                country.Continent = "Not specified";
            }
            
            ValidateCountry(country);
            Country maybeCountry = RetrieveMatchingCountry(country);

            if(maybeCountry is null)
            {
                return await this.countryService.AddCountryAsync(country);
            }
            else
            {
                country.Id = maybeCountry.Id;
                country.CreatedDate = maybeCountry.CreatedDate;
                return await this.countryService.ModifyCountryAsync(country);
            }
        });

        private Country RetrieveMatchingCountry(Country country)
        {
            IQueryable<Country> countries =
                this.countryService.RetrieveAllCountries();

            return countries.FirstOrDefault(SameCountryAs(country));
        }

        private static Expression<Func<Country, bool>> SameCountryAs(Country country) =>
            retrievedCountry => retrievedCountry.Name == country.Name;
        
        private static bool IsCountryChanged(Country incomingCountry, Country existingCountry)
        {
            return (incomingCountry, existingCountry) switch
            {
                _ when incomingCountry.Cases != existingCountry.Cases => true,
                _ when incomingCountry.Deaths != existingCountry.Deaths => true,
                _ when incomingCountry.Recovered != existingCountry.Recovered => true,
                _ => false
            };
        }
    }
}
