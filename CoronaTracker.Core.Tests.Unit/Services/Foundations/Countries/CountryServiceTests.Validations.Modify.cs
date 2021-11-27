using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task ShouldThrowValidationExceptionOnModifyIfCountryIsNullAndLogItAsync()
        {
            //given
            Country nullcountry = null;
            var nullCountryException = new NullCountryException();

            var expectedCountryValdationException =
                new CountryValidationException(nullCountryException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(nullcountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValdationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfCountryIsInvalidAndLogItAsync(string invalidText)
        {
            // given 
            var invalidCountry = new Country
            {
                Name = invalidText,
                Iso3 = invalidText,
                Continent = invalidText
            };

            var invalidCountryException = new InvalidCountryException();

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
                "Date is required",
                $"Date is the same as {nameof(Country.CreatedDate)}");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            // when
            ValueTask<Country> modifyCountryTask =
                this.countryService.ModifyCountryAsync(invalidCountry);

            // then
            await Assert.ThrowsAsync<CountryValidationException>(() =>
                modifyCountryTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTimeOffset(),
                   Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCountryValidationException))),
                        Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateCountryAsync(It.IsAny<Country>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls(); 
        }
    }
}
