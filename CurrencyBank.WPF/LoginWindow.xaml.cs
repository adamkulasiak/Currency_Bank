using CurrencyBank.WPF.Dto;
using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CurrencyBank.WPF
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private AuthService _authService;
        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            var userLoginDto = new UserLoginDto()
            {
                UserName = UserName_tb.Text,
                Password = password_tb.Password
            };

            var response = _authService.Login(userLoginDto);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            var loggedInUser = new LoggedInUser()
            {
                Token = (string)json.SelectToken("token"),
            };
        }

    }
}
