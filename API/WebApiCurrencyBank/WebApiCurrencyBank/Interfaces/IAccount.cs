using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCurrencyBank.Interfaces
{
    public interface IAccount
    {
        Task<Account> Create(int userId, Currency currency);
        Task<Account> CashIn(int userId, int accountId, decimal ammount);
        Task<Account> CashOut(int userId, int accountId, decimal ammount);
        Task<bool> DeleteAccount(int userId, int accountId);
        Task<IList<Account>> ExchangeMoney(int userId, int sourceAccountId, int destinationAccountId, decimal ammount);
    }
}
