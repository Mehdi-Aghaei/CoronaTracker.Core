using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using Microsoft.Data.SqlClient;
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
            catch (InvalidCountryException invalidCountryInputException)
            {
                throw CreateAndLogValidationException(invalidCountryInputException);
            }
            catch(SqlException sqlException)
            {
                var failedCountryStorageException = 
                    new FailedCountryStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedCountryStorageException);
            }
        }

        private CountryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var countryDependencyExceptin = new CountryDependencyException(exception);

            this.loggingBroker.LogCritical(countryDependencyExceptin);

            return countryDependencyExceptin;
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
