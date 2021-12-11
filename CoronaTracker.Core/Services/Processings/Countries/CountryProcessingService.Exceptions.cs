// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Processings.Countries
{
    public partial class CountryProcessingService
    {
        private delegate ValueTask<Country> ReturningCountryFunction();

        private async ValueTask<Country> TryCatch(ReturningCountryFunction returningCountryFunction)
        {
            try
            {
                return await returningCountryFunction();
            }
            catch (NullCountryProcessingException nullCountryProcessingException)
            {
                throw CreateAndLogValidationException(nullCountryProcessingException);
            }
            catch (InvalidCountryProcessingException invalidCountryProcessingException)
            {
                throw CreateAndLogValidationException(invalidCountryProcessingException);
            }
            catch( CountryValidationException countryValidationException)
            {
                throw CreateAndLogDependencyValidationException(countryValidationException);
            }
            catch(CountryDependencyValidationException countryDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(countryDependencyValidationException);
            }
            catch(CountryDependencyException countryDependencyException)
            {
                throw CreateAndLogDependencyException(countryDependencyException);
            }
            catch(CountryServiceException countryServiceException)
            {
               throw CreateAndLogDependencyException(countryServiceException);
            }
        }

        private CountryProcessingDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var countryProcessingDependencyValidationException =
                new CountryProcessingDependencyValidationException(
                    exception.InnerException as Xeption);
            
            this.loggingBroker.LogError(countryProcessingDependencyValidationException);

            return countryProcessingDependencyValidationException;
        }  
        
        private CountryProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var countryProcessingDependencyException =
                new CountryProcessingDependencyException(
                    exception.InnerException as Xeption);
            
            this.loggingBroker.LogError(countryProcessingDependencyException);

            return countryProcessingDependencyException;
        }

        private CountryProcessingValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var countryProcessingValidationException =
                new CountryProcessingValidationException(exception);

            this.loggingBroker.LogError(countryProcessingValidationException);

            return countryProcessingValidationException;
        }
    }
}
