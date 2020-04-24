using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Dtos
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
        public Languages Language { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public string PathToPdfFolder { get; set; }
    }
}
