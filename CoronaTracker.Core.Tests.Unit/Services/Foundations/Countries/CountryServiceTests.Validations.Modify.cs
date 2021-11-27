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
    }
}
