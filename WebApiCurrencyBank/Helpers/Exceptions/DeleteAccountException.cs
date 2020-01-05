using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers.Exceptions
{
    public class DeleteAccountException: Exception
    {
        public DeleteAccountException() { }

        public DeleteAccountException(string message) : base(message) { }
    }
}
