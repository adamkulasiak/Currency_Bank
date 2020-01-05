using CurrencyBank.WPF.Dto;
using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private AuthService _authService;
        public RegisterWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void CreateRegisterForm()
        {
            FirstName_tb.Text = "";
            LastName_tb.Text = "";
            UserName_tb.Text = "";
            Email_tb.Text = "";
            Pesel_tb.Text = "";
            password_tb.Password = "";
        }
        private async void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            Register_btn.IsEnabled = false;
            var userRegisterDto = new UserRegisterDto()
            {
                FirstName = FirstName_tb.Text,
                LastName = LastName_tb.Text,
                UserName = UserName_tb.Text,
                Email = Email_tb.Text,
                Pesel = Pesel_tb.Text,
                Password = password_tb.Password
            };

            var response = await _authService.Register(userRegisterDto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Pomyślnie zarejestrowano");

                var userLoginDto = new UserLoginDto { UserName = UserName_tb.Text, Password = password_tb.Password };
                var resp = await _authService.Login(userLoginDto);

                JObject json = JObject.Parse(resp.Content.ReadAsStringAsync().Result);
                JObject jsonObj = (JObject)json["userToReturn"];
                var loggedInUser = jsonObj.ToObject<LoggedInUser>();

                MainWindow mainWindow = new MainWindow(loggedInUser);
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result.ToString());
                Register_btn.IsEnabled = true;
            }

        }
    }
}
