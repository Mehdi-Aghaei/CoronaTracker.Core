using System;
using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace CoronaTracker.Core.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private delegate IQueryable<Country> ReturningCountriesFunction();
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
            catch (SqlException sqlException)
            {
                var failedCountryStorageException =
                    new FailedCountryStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedCountryStorageException);
            }
            catch (NotFoundCountryException notFoundCountryException)
            {
                throw CreateAndLogValidationException(notFoundCountryException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsCountryException =
                    new AlreadyExistsCountryException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsCountryException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedCountryStorageException =
                    new FailedCountryStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedCountryStorageException);
            }
            catch (Exception exception)
            {
                var failedCountryServiceException =
                    new FailedCountryServiceException(exception);

                throw CreateAndLogServiceException(failedCountryServiceException);
            }
        }

        private IQueryable<Country> TryCatch(ReturningCountriesFunction returningCountriesFunction)
        {
            try
            {
                return returningCountriesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedCountryStorageException =
                    new FailedCountryStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedCountryStorageException);
            }
            catch (Exception exception)
            {
                var failedCountryServiceException =
                   new FailedCountryServiceException(exception);

                throw CreateAndLogServiceException(failedCountryServiceException);
            }
        }

        private CountryDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var countryDependencyValidationException =
                new CountryDependencyValidationException(exception);

            this.loggingBroker.LogError(countryDependencyValidationException);

            return countryDependencyValidationException;
        }

        private CountryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var countryDependencyException = new CountryDependencyException(exception);

            this.loggingBroker.LogCritical(countryDependencyException);

            return countryDependencyException;
        }
        private CountryDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var countryDependencyException = new CountryDependencyException(exception);

            this.loggingBroker.LogError(countryDependencyException);

            return countryDependencyException;
        }

        private CountryValidationException CreateAndLogValidationException(Xeption exception)
        {
            var countryValidationException =
                new CountryValidationException(exception);

            this.loggingBroker.LogError(countryValidationException);

            return countryValidationException;
        }
        private CountryServiceException CreateAndLogServiceException(Xeption exception)
        {
            var countryServiceException =
                new CountryServiceException(exception);

            this.loggingBroker.LogError(countryServiceException);

            return countryServiceException;
        }
    }
}
