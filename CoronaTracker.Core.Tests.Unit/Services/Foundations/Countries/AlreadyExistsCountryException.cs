using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace CoronaTracker.Core.Tests.Unit.Services.Foundations.Countries
{
    public class AlreadyExistsCountryException : Xeption
    {
        public AlreadyExistsCountryException(Exception innerException)
             : base(message: "Country with the same id already exists.", innerException)
        { }
    }
}
