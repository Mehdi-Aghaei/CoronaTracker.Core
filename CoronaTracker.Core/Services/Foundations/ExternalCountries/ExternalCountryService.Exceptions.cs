using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.ExternalCountries;
using CoronaTracker.Core.Models.ExternalCountries.Exceptions;
using CoronaTrackerHungary.Web.Api.Models.Countries.Exceptions;
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
                var failedExternalCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedExternalCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedExternalCountryDependencyExcpetion =
                    new FailedExternalCountryDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedExternalCountryDependencyExcpetion);
            }
            catch (HttpResponseInternalServerErrorException httpResponseInternalServerErrorException)
            {
                var failedExternalCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpResponseInternalServerErrorException);

                throw CreateAndLogDependencyException(failedExternalCountryDependencyException);

            }
            catch (HttpResponseException httpResponseException)
            {
                var failedExternalCountryDependencyException =
                    new FailedExternalCountryDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedExternalCountryDependencyException);
            }
            catch (Exception serviceException)
            {
                var failedExternalCountryServiceException =
                    new FailedExternalCountryServiceException(serviceException);

                throw CreateAndLogServiceException(failedExternalCountryServiceException);
            }
        }

        private ExternalCountryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var externalCountryDependencyException =
                new ExternalCountryDependencyException(exception);

            this.loggingBroker.LogCritical(externalCountryDependencyException);

            return externalCountryDependencyException;
        }

        private ExternalCountryDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var externalCountryDependencyException =
                new ExternalCountryDependencyException(exception);

            this.loggingBroker.LogError(externalCountryDependencyException);

            return externalCountryDependencyException;
        }

        private ExternalCountryServiceException CreateAndLogServiceException(Exception exception)
        {
            var externalCountryServiceException =
                new ExternalCountryServiceException(exception);

            this.loggingBroker.LogError(externalCountryServiceException);

            return externalCountryServiceException;
        }
    }
}
