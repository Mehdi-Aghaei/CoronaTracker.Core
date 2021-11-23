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
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfCountryIsInvalidAndLogItAsync(string invalidText)
        {
            // given
            var invalidCountry = new Country
            {
                Name = invalidText,
                Iso3 = invalidText,
                Continent = invalidText
            };
            var invalidCountryException =
                new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.Id),
                values: "Id is required");

            invalidCountryException.AddData(
                key: nameof(Country.Name),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.Iso3),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.Continent),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.CreatedDate),
                values: "Date is required");

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: "Date is required");

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
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreateAndUpdateDateIsNotSameAndLogItAsync()
        {
            // give
            int randomNumber = GetRandomNumber();
            Country randomCountry = CreateRandomCountry();
            Country invalidCountry = randomCountry;

            invalidCountry.UpdatedDate =
                invalidCountry.CreatedDate.AddDays(randomNumber);

            var invalidCountryException =
                new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: $"Date is not the same as {nameof(Country.CreatedDate)}");

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
