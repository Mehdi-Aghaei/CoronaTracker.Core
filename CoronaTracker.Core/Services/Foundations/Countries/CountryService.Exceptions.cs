using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private delegate ValueTask<Country> ReturningCountryFunction();
        
        private async ValueTask<Country> TryCatch(ReturningCountryFunction returningCountryFunction)
        {
            try
            {
                return await returningCountryFunction();
            }
            catch (NullCountryException nullCountryException)
            {

                throw CreateAndLogValidationException(nullCountryException);
            }
        }

        private void ValidateCountryOnAdd(Country country)
        {
            validateCountryIsNotNull(country);
        }

        private void validateCountryIsNotNull(Country country)
        {
            if(country is null)
            {
                throw new NullCountryException();
            }
        }

        private CountryValidationException CreateAndLogValidationException(Xeption exception)
        {
            var countryValidationException =
                new CountryValidationException(exception);

            this.loggingBroker.LogError(countryValidationException);

            return countryValidationException;
        }
    }
}
