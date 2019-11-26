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
        private const string baseUrl = "http://api.nbp.pl/api/exchangerates/rates/A/";
        public ExchangeRateRepository()
        {
            _client = new HttpClient();
        }

        //todo
        public async Task<bool> GetRatesFromNbp()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(baseUrl + "USD");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject objectFromJson = JObject.Parse(responseBody);
                var rate = objectFromJson.ToString();
                return true;
            }
            catch (HttpRequestException) { return false; }
        }
    }
}
