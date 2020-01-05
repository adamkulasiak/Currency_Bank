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
    public class AuthService
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _baseUrl = "http://localhost:5000/api/auth";

        public async Task<HttpResponseMessage> Login(UserLoginDto userLoginDto)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await _client.PostAsJsonAsync(_baseUrl + "/login", userLoginDto);
        }

        public async Task<HttpResponseMessage> Register(UserRegisterDto userRegisterDto)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await _client.PostAsJsonAsync(_baseUrl + "/register", userRegisterDto);
        }
    }
}
