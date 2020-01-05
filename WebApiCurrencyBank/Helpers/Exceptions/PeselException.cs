using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers.Exceptions
{
    public class PeselException: Exception
    {
        public PeselException() { }

        public PeselException(string message) : base(message) { }
    }
}
