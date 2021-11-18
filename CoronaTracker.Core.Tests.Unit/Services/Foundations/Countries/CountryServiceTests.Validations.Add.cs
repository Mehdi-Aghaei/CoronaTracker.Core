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

        [Theory]
        [InlineData (null)]
        [InlineData("")]
        [InlineData (" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfCountryIsInvalidAndLogItAsync(string invalidText) 
        {
            // given
            var invalidCountry = new Country
            {
                CountryName = invalidText,
                Iso3 = invalidText,
                Continent = invalidText
            };
            var invalidCountryException =
                new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.Id),
                values: "Id is required");

            invalidCountryException.AddData(
                key: nameof(Country.CountryName),
                values: "Name is required");

            invalidCountryException.AddData(
                key: nameof(Country.Iso3),
                values: "Iso3 is required");  
            
            invalidCountryException.AddData(
                key: nameof(Country.Continent),
                values: "Continent is required");   
            
            invalidCountryException.AddData(
                key: nameof(Country.Cases),
                values: "Cases is required");     
            
            invalidCountryException.AddData(
                key: nameof(Country.TodayCases),
                values: "TodayCases is required"); 
            
            invalidCountryException.AddData(
                key: nameof(Country.Deaths),
                values: "Deaths is required"); 
            
            invalidCountryException.AddData(
                key: nameof(Country.TodayDeaths),
                values: "TodayDeaths is required"); 
            
            invalidCountryException.AddData(
                key: nameof(Country.Recovered),
                values: "Recovered is required"); 
            
            invalidCountryException.AddData(
                key: nameof(Country.TodayRecovered),
                values: "TodayRecovered is required"); 
            
            invalidCountryException.AddData(
                key: nameof(Country.Population),
                values: "Population is required");  
            
            invalidCountryException.AddData(
                key: nameof(Country.CasesPerOneMillion),
                values: "CasesPerOneMillion is required");

            invalidCountryException.AddData(
              key: nameof(Country.DeathsPerOneMillion),
              values: "DeathsPerOneMillion is required");

            invalidCountryException.AddData(
                key: nameof(Country.RecoveredPerOneMillion),
                values: "RecoveredPerOneMillion is required");  
            
            
            invalidCountryException.AddData(
                key: nameof(Country.CriticalPerOneMillion),
                values: "TodayRecovered is required");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            // when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                addCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
