// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Foundations.Countries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnUpsertIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            var someCountry = CreateRandomCountry();

            var expectedCountryProcessingDependencyValidationException =
                new CountryProcessingDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Throws(dependencyValidationException);

            // when
            ValueTask<Country> upsertCountryTask =
                this.countryProcessingService.UpsertCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryProcessingDependencyValidationException>(() =>
               upsertCountryTask.AsTask());

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryProcessingDependencyValidationException))),
                        Times.Once);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.ModifyCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnUpsertIfDependencyErrorOccursAndLogItAsync(
           Xeption dependencyException)
        {
            // given
            var someCountry = CreateRandomCountry();

            var expectedCountryProcessingDependencyException =
                new CountryProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Throws(dependencyException);

            // when
            ValueTask<Country> upsertCountryTask =
                this.countryProcessingService.UpsertCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryProcessingDependencyException>(() =>
               upsertCountryTask.AsTask());

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryProcessingDependencyException))),
                        Times.Once);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.ModifyCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnUpsertIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someCountry = CreateRandomCountry();

            var serviceException = new Exception();

            var failedCountryProcessingServiceException =
                new FailedCountryProcessingServiceException(serviceException);

            var expectedCountryProcessingServiveException =
                new CountryProcessingServiceException(
                    failedCountryProcessingServiceException);

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Throws(serviceException);

            // when
            ValueTask<Country> upsertCountryTask =
                this.countryProcessingService.UpsertCountryAsync(someCountry);

            // then
            await Assert.ThrowsAsync<CountryProcessingServiceException>(() =>
               upsertCountryTask.AsTask());

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryProcessingServiveException))),
                        Times.Once);

            this.countryServiceMock.Verify(service =>
                service.AddCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.Verify(service =>
                service.ModifyCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
