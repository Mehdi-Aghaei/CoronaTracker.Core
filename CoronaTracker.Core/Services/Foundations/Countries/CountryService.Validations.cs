using System;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private void ValidateCountryOnAdd(Country country)
        {
            ValidateCountryIsNotNull(country);

            Validate(
            (Rule: IsInvalid(country.Id), Parameter: nameof(country.Id)),
            (Rule: IsInvalid(country.Name), Parameter: nameof(country.Name)),
            (Rule: IsInvalid(country.Iso3), Parameter: nameof(country.Iso3)),
            (Rule: IsInvalid(country.Continent), Parameter: nameof(country.Continent)),
            (Rule: IsInvalid(country.CreatedDate), Parameter: nameof(country.CreatedDate)),
            (Rule: IsInvalid(country.UpdatedDate), Parameter: nameof(country.UpdatedDate)));
        }

        private void ValidateCountryIsNotNull(Country country)
        {
            if (country is null)
            {
                throw new NullCountryException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCountryException = new InvalidCountryException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCountryException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }
            invalidCountryException.ThrowIfContainsErrors();
        }
    }
}
