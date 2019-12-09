using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Interfaces
{
    public interface IExchangeRate
    {
        Task<decimal> ChangeMoney(Currency src, Currency dest);
    }
}
