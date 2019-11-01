using CurrencyBank.BLL.Interfaces;
using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBank.BLL.Managers
{
    public class AuthManager : IAuth
    {
        private readonly CurrencyBankContext _context;
        public AuthManager()
        {
            _context = new CurrencyBankContext();
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda do rejestracji uzytkownika
        /// </summary>
        /// <param name="user">obiekt użytkownika</param>
        /// <param name="password">hasło</param>
        /// <returns></returns>
        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHashSalt(password, out PasswordHash, out PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            user.UserName = user.UserName.ToLower();

            if (!await UserExists(user.UserName))
            {
                await _context.Users.AddAsync(user);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException exception)
                {
                    return null;
                }
                
                return user;
            }
            return null;
        }

        /// <summary>
        /// Sprawdza czy podany użytkownik istnieje w bazie
        /// </summary>
        /// <param name="username">nazwa użytkownika</param>
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == username))
                return true;

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
