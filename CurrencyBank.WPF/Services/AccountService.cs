using CurrencyBank.WPF.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBank.WPF.Services
{
    public class AccountService
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _baseUrl = "http://localhost:5000/api/account";

        public async Task<HttpResponseMessage> GetAccountsForUser(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync(_baseUrl + "/getAccounts");

            return response;
        }
    }
}
