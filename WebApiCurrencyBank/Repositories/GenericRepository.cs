using CurrencyBank.API.Interfaces;
using CurrencyBank.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly CurrencyBankContext _context;
        public GenericRepository(CurrencyBankContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
