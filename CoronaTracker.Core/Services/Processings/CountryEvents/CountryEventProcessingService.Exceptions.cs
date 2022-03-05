// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.CountryEvents;
using CoronaTracker.Core.Models.Processings.CountryEvents;
using Xeptions;

namespace CoronaTracker.Core.Services.Processings.CountryEvents
{
    public partial class CountryEventProcessingService
    {
        private delegate ValueTask<CountryEvent> ReturningCountryEventFunction();
        
        private async ValueTask<CountryEvent> TryCatch(ReturningCountryEventFunction returningCountryEventFunction)
        {
            try
            {
                return await returningCountryEventFunction();
            }
            catch (NullCountryEventProcessingException nullCountryEventProcessingException)
            {

                throw CreateAndLogCountryEventValidationException(
                    nullCountryEventProcessingException);
            }
        }

        private CountryEventProcessingValidationException CreateAndLogCountryEventValidationException(Xeption exception)
        {
            var countryEventProcessingValidationException = 
                new CountryEventProcessingValidationException(exception);

            this.loggingBroker.LogError(countryEventProcessingValidationException);

            return countryEventProcessingValidationException;
        }
    }
}
