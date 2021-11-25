using System.Linq;
using CoronaTracker.Core.Models.Countries;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllCountries()
        {
            // given
            IQueryable<Country> randomCountries = CreateRandomCountries();
            IQueryable<Country> storageCounries = randomCountries;
            IQueryable<Country> expectedCounries = storageCounries.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCountries())
                    .Returns(storageCounries);

            // when
            IQueryable<Country> actualCountries =
                this.countryService.RetrieveAllCountries();

            // then
            actualCountries.Should().BeEquivalentTo(expectedCounries);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCountries(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
