using CurrencyBank.API.Dtos;
using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountForUser(int userId);
        Task<List<AccountHistoryDto>> GetAccountsHistoryForUser(int userId);
        Task<Account> Create(int userId, Currency currency);
        Task<Account> CashIn(int userId, int accountId, decimal ammount);
        Task<Account> CashOut(int userId, int accountId, decimal ammount);
        Task<bool> DeleteAccount(int userId, int accountId);
        Task<IList<Account>> ExchangeMoney(int userId, int sourceAccountId, int destinationAccountId, decimal ammount);
        Task<IList<Account>> DoTransferMoney(int principalId, int principalAccountId, string destinationAccountNumber, decimal ammount);
    }
}
