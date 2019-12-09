using CurrencyBank.WPF.Dto;
using CurrencyBank.WPF.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CurrencyBank.WPF.Services
{
    public class AuthService
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _baseUrl = "http://localhost:5000/api/auth";

        public HttpResponseMessage Login(UserLoginDto userLoginDto)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client.PostAsJsonAsync(_baseUrl + "/login", userLoginDto).Result;
        }
        
    }
}
