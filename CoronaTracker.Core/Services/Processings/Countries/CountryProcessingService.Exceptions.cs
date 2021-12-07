// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
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
        }

        private CountryProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var countryProcessingValidationException = 
                new CountryProcessingValidationException(exception);

            this.loggingBroker.LogError(countryProcessingValidationException);

            return countryProcessingValidationException;
        }
    }
}
