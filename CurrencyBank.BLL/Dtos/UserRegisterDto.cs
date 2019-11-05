using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.BLL.Dtos
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public string Password { get; set; }
    }
}
