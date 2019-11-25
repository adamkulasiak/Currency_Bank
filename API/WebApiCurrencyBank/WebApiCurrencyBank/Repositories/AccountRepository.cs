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
    public class AccountRepository : IAccount
    {
        private readonly CurrencyBankContext _context;
        public AccountRepository(CurrencyBankContext context)
        {
            _context = context;
        }

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
                User = currentUser,
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
    }
}
