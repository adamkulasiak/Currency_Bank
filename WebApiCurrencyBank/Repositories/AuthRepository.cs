using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyBank.Database.Data;
using CurrencyBank.Database.Models;
using Microsoft.EntityFrameworkCore;
using CurrencyBank.API.Interfaces;
using Z.EntityFramework.Plus;
using CurrencyBank.Commons;
using CurrencyBank.API.Helpers.Exceptions;

namespace CurrencyBank.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CurrencyBankContext _context;
        public AuthRepository(CurrencyBankContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.IncludeFilter(x => x.Accounts.Where(y => !y.IsDeleted)).FirstOrDefaultAsync(x => x.UserName == username);

            var sql = user.ToString();

            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHashSalt(password, out PasswordHash, out PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            if ((_context.Users.Any(x => x.Email == user.Email)) || (EmailValidator.IsEmailValid(user.Email) == false))
            {
                throw new EmailException("Email is not valid or exists user with the same email");
            }

            if (_context.Users.Any(x => x.Pesel == user.Pesel) || PeselValidator.IsPeselValid(user.Pesel) == false)
            {
                throw new PeselException("Pesel is not valid or exists user with the same pesel");
            }

            if (_context.Users.Any(x => x.UserName == user.UserName))
            {
                throw new UsernameException("User with this username already exists");
            }

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException e)
            {
                return null;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username)) return true;

            return false;
        }

        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}
