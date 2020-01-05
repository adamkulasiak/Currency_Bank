using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers.Exceptions
{
    public class EmailException: Exception
    {
        public EmailException() {}

        public EmailException(string message): base(message) { }
    }
}
