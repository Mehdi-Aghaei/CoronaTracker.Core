// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Processings.Countries.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public void ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogIt(
           Xeption dependencyException)
        {
            // given
            var someCountry = CreateRandomCountries();

            var expectedCountryProcessingDependencyException =
                new CountryProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Throws(dependencyException);

            // when
            Action retrieveAllCountriesAction = () =>
                this.countryProcessingService.RetrieveAllCountries();

            // then
            Assert.Throws<CountryProcessingDependencyException>(retrieveAllCountriesAction);

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryProcessingDependencyException))),
                        Times.Once);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
