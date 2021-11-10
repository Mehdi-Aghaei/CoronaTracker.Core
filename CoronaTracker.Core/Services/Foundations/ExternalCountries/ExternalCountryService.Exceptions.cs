using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountries.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountries
{
    public partial class ExternalCountryService
    {
        private delegate ValueTask<List<ExternalCountry>> ReturningExternalCountriesFunction();

        private async ValueTask<List<ExternalCountry>> TryCatch(ReturningExternalCountriesFunction returningExternalCountriesFunction)
        {
            try
            {
                return await returningExternalCountriesFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCountryDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCountryDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCountryDependencyExcpetion =
                    new FailedExternalCountryDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCountryDependencyExcpetion);
            }
        }

        private ExternalCountryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var countryDependencyException =
                new ExternalCountryDependencyException(exception);

            this.loggingBroker.LogCritical(countryDependencyException);

            return countryDependencyException;
        }
    }
}
