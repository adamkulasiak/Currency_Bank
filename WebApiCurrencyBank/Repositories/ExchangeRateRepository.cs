using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Repositories
{
    public class ExchangeRateRepository : IExchangeRate
    {
        private readonly HttpClient _client;
        private readonly CurrencyBankContext _context;
        private const string baseUrl = "http://api.exchangeratesapi.io/latest/";
        public ExchangeRateRepository(CurrencyBankContext context)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _context = context;
        }

        /// <summary>
        /// Metoda zamieniajaca waluty
        /// </summary>
        /// <param name="src">waluta zrodlowa</param>
        /// <param name="dest">waluta docelowa</param>
        /// <returns>kurs</returns>
        public async Task<decimal> ChangeMoney(Currency src, Currency dest)
        {
            if (_context.ExchangeRates.Where(x => x.From == src && x.To == dest && x.DateTime == DateTime.Today).Any())
            {
                var data = await _context.ExchangeRates.Where(x => x.From == src && x.To == dest && x.DateTime == DateTime.Today).SingleOrDefaultAsync();
                return data.Rate;
            }
            var response = _client.GetAsync("?base="+src.ToString()).Result;
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            var rate = (decimal)json.SelectToken($"rates.{dest.ToString()}");

            var d = new ExchangeRates
            {
                DateTime = DateTime.Today,
                From = src,
                To = dest,
                Rate = rate
            };

            _context.Add(d);
            try
            {
                await _context.SaveChangesAsync();
                return rate;
            }
            catch (DbUpdateException e) { Console.WriteLine(e.ToString()); return 0; }
        }
    }
}
