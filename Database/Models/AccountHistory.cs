using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Database.Models
{
    public class AccountHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public decimal Ammount { get; set; }
    }
}
