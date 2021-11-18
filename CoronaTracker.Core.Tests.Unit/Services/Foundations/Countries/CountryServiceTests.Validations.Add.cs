using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using CoronaTracker.Core.Models.Countries.Exceptions;
using Moq;
using Xunit;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCountryIsNullAndLogItAsync()
        {
            // given 
            Country nullCountry = null;

            var nullCountryException =
                new NullCountryException();

            var expectedCountryValidationException =
                new CountryValidationException(nullCountryException);

            // when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(nullCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
               addCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
