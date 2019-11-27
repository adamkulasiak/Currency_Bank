using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCurrencyBank.Interfaces
{
    public interface IExchangeRate
    {
        Task<string> ChangeMoney(Currency src, Currency dest);
    }
}
