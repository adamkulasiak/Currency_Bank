using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers.Exceptions
{
    public class BalanceException: Exception
    {
        public BalanceException() { }

        public BalanceException(string message) : base(message) { }
    }
}
