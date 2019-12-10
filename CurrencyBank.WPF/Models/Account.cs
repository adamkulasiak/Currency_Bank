using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.WPF.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }

    public enum Currency : byte
    {
        PLN = 1,
        EUR,
        USD,
        GBP
    }
}
