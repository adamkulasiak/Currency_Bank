using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Repositories
{
    public class ExchangeRateRepository : IExchangeRate
    {
        private readonly HttpClient _client;
        private readonly CurrencyBankContext _context;
        private const string baseUrl = "http://api.nbp.pl/api/exchangerates/rates/A/";
        public ExchangeRateRepository(CurrencyBankContext context)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _context = context;
        }

        /// <summary>
        /// Metoda pobierajaca dane o kursie euro i zapisujace do lokalnej bazy
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetEURRateFromNbp() => await GetDataFromNBP(Currency.EUR);

        /// <summary>
        /// Metoda pobierajaca dane o kursie dolara i zapisujace do lokalnej bazy
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUSDRateFromNbp() => await GetDataFromNBP(Currency.USD);

        /// <summary>
        /// Metoda pobierajaca dane o kursie funta i zapisujace do lokalnej bazy
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetGBPRateFromNbp() => await GetDataFromNBP(Currency.GBP);

        private async Task<string> GetDataFromNBP(Currency currency)
        {
            var response = _client.GetAsync(currency.ToString()).Result;
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            var USDrates = (decimal)json.SelectToken("rates[0].mid");

            var d = new ExchangeRates
            {
                DateTime = DateTime.Now,
                Currency = currency,
                Mid = USDrates
            };

            _context.Add(d);
            try
            {
                await _context.SaveChangesAsync();
                return $"{d.Currency} {d.Mid}";
            }
            catch (DbUpdateException) { return null; }
        }
    }
}
