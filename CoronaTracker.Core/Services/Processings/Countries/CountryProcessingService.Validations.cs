// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public partial class CountryProcessingService
    {
        private static void ValidateCountry(Country country)
        {
            ValidateCountryIsNotNull(country);

            Validate(
                (Rule: IsInvalid(country.Id),
                Parameter: nameof(Country.Id)));
        }

        private void ValidateCountryOnVerify(Country Country)
        {
            ValidateCountryIsNotNull(Country);
        }

        private static void ValidateCountryIsNotNull(Country country)
        {
            if (country is null)
            {
                throw new NullCountryProcessingException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCountryProcessingException =
                new InvalidCountryProcessingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCountryProcessingException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidCountryProcessingException.ThrowIfContainsErrors();
        }
    }
}
