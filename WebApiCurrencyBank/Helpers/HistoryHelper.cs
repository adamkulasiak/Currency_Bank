using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Helpers
{
    public class HistoryHelper
    {
        public AccountHistory Log(Account account, decimal ammount)
        {
            var entry = new AccountHistory
            {
                Timestamp = DateTime.Now,
                AccountId = account.Id,
                Account = account,
                Ammount = ammount
            };
            return entry;
        }
    }
}
