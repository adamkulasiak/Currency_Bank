using System;

namespace CurrencyBank.WPF.Models
{
    class LoggedInUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
    }
}
