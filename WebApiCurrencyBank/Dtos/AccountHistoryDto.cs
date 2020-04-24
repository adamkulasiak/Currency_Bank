using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Dtos
{
    public class AccountHistoryDto
    {
        public int AcoountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public List<AccountHistory> History { get; set; }
    }
}
