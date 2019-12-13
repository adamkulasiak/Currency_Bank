using CurrencyBank.API.Interfaces;
using CurrencyBank.Database.Data;
using CurrencyBank.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly CurrencyBankContext _context;
        public UserRepository(CurrencyBankContext context): base(context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Accounts).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            if (await base.SaveAll())
                return user;

            return null;
        }
    }
}
