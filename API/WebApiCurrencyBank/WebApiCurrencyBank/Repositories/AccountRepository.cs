using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Repositories
{
    /// <summary>
    /// Repozytorium do zarzadzania operacjami na koncie
    /// </summary>
    public class AccountRepository : IAccount
    {
        private readonly CurrencyBankContext _context;
        public AccountRepository(CurrencyBankContext context)
        {
            _context = context;
        }
        #region public methods
        /// <summary>
        /// Metoda sluzaca do stworzenia konta
        /// </summary>
        /// <param name="userId">id usera dla ktorego ma byc utworzone konto</param>
        /// <param name="currency">waluta</param>
        /// <returns>konto jezeli zostanie utworzone pomyslnie, null w przeciwnym wypadku</returns>
        public async Task<Account> Create(int userId, Currency currency)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (currentUser is null)
                return null;

            var account = new Account
            {
                AccountNumber = AccountNumberGenerator(),
                Balance = 0,
                Currency = currency,
                UserId = userId
            };

            _context.Add(account);
            try
            {
                await _context.SaveChangesAsync();
                return account;
            }
            catch (DbUpdateException)
            {
                return null;
            }

        }

        /// <summary>
        /// Metoda sluzaca do zasilenia konta nowymi srodkami pienieznymi
        /// </summary>
        /// <param name="userId">id wlasciciela konta</param>
        /// <param name="accountId">id konta</param>
        /// <param name="ammount">kwota</param>
        /// <returns>konto</returns>
        public async Task<Account> CashIn(int userId, int accountId, decimal ammount)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account.UserId != userId)
                return null;

            account.Balance += ammount;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return null; }

            return account;
        }
        /// <summary>
        /// Metoda sluzaca do wyplacenia pieniedzy z konta
        /// </summary>
        /// <param name="userId">id wlasciciela konta</param>
        /// <param name="accountId">id konta</param>
        /// <param name="ammount">kwota</param>
        /// <returns></returns>
        public async Task<Account> CashOut(int userId, int accountId, decimal ammount)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account.UserId != userId)
                return null;

            if (account.Balance < ammount)
                return null;
            account.Balance -= ammount;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return null; }
            return account;
        }

        /// <summary>
        /// Metoda sluzaca do usuwania(zmiany atrybutu na usuniety) konta
        /// </summary>
        /// <param name="userId">id wlasciciela</param>
        /// <param name="accountId">id konta</param>
        /// <returns></returns>
        public async Task<bool> DeleteAccount(int userId, int accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account?.UserId != userId)
                return false;

            account.IsDeleted = true;
            account.DeleteTime = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException) { return false; }
        }

        #endregion

        #region private methods
        private string AccountNumberGenerator()
        {
            BigInteger temp;
            string accountNumber;

            if (_context.Accounts.Any())
            {
                accountNumber = _context.Accounts.Max(x => x.AccountNumber);
                temp = BigInteger.Parse(accountNumber);
                temp++;
                return temp.ToString();
            }
            else
            {
                accountNumber = "2210000000000000000000000000";
                return accountNumber;
            } 
        }
        #endregion
    }
}
