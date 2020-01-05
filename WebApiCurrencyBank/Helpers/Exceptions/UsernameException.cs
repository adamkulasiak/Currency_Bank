using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers.Exceptions
{
    public class UsernameException: Exception
    {
        public UsernameException() { }

        public UsernameException(string message) : base(message) { }
    }
}
