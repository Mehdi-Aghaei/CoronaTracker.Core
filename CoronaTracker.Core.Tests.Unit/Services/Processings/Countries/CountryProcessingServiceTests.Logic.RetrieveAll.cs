// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Processings.Countries
{
    public partial class CountryProcessingServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllCountries()
        {
            // given
            IQueryable<Country> randomCountries = CreateRandomCountries();
            IQueryable<Country> storageCountries = randomCountries;
            IQueryable<Country> expectedCountries = storageCountries;

            this.countryServiceMock.Setup(service =>
                service.RetrieveAllCountries())
                    .Returns(storageCountries);

            // when
            IQueryable<Country> actualCountries = 
                this.countryProcessingService.RetrieveAllCountries();

            // then
            actualCountries.Should().BeEquivalentTo(expectedCountries);

            this.countryServiceMock.Verify(service =>
                service.RetrieveAllCountries(),
                    Times.Once);

            this.countryServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
