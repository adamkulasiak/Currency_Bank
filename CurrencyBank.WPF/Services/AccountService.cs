using CurrencyBank.WPF.Dto;
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

        public async Task<HttpResponseMessage> Withdrawal(string token, int accountId, decimal ammount)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.PutAsync(_baseUrl + "/cashout?accountId=" + accountId + "&ammount="+ ammount, null);

            return response;
        }

        public async Task<HttpResponseMessage> OpenAccount(string token, AccountToCreateDto accountToCreateDto)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new
            {
                Currency = accountToCreateDto.Currency
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(_baseUrl + "/create", data);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAccount(string token, int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.DeleteAsync(_baseUrl + "/deleteAccount?accountId=" + id);

            return response;
        }
    }
}
