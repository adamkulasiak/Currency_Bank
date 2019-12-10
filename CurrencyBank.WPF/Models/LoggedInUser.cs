using System;
using System.Collections.Generic;

namespace CurrencyBank.WPF.Models
{
    public class LoggedInUser
    {
        public LoggedInUser()
        {
            Accounts = new List<Account>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
        public List<Account> Accounts { get; set; }

        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {UserName} {Email} {Pesel} {CreatedDate} /n {Token} /n {Accounts}";
        }
    }
}
