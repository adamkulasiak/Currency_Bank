using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyBank.Database.Data
{
    public class Seed
    {
        private readonly CurrencyBankContext _context;
        public Seed(CurrencyBankContext context)
        {
            _context = context;
        }
        
        public void SeedData()
        {
            if (!_context.Users.Any())
            {
                IList<User> users = new List<User>
            {
                new User{ FirstName = "Jan", LastName = "Kowalski", UserName = "jkowalski", Email = "j.kowalski@gmail.com", Pesel = "99012388777" },
                new User{ FirstName = "Maria", LastName = "Nowak", UserName = "mnowak", Email = "m.nowak@gmail.com", Pesel = "99012388123" },
                new User{ FirstName = "Wacław", LastName = "Nowacki", UserName = "wnowacki", Email = "w.nowacki@gmail.com", Pesel = "99012388222" }
            };

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHashSalt("test12345", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _context.Add(user);
                }
                _context.SaveChanges();
            }
            if (!_context.Accounts.Any())
            {
                IList<Account> accounts = new List<Account>
                {
                    new Account{ AccountNumber = "2210000000000000000000000000", Balance = 0, Currency = Currency.PLN, UserId = 1 },
                    new Account{ AccountNumber = "2210000000000000000000000001", Balance = 0, Currency = Currency.USD, UserId = 1 },
                    new Account{ AccountNumber = "2210000000000000000000000002", Balance = 0, Currency = Currency.PLN, UserId = 2 },
                    new Account{ AccountNumber = "2210000000000000000000000003", Balance = 0, Currency = Currency.PLN, UserId = 3 },
                };

                _context.AddRange(accounts);
                _context.SaveChanges();
            }
        }

        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
    }
}
