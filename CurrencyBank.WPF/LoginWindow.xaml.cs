using CurrencyBank.WPF.Dto;
using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            //to remove later
            UserName_tb.Text = "jkowalski";
            password_tb.Password = "test12345";
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            var userLoginDto = new UserLoginDto()
            {
                UserName = UserName_tb.Text,
                Password = password_tb.Password
            };

            var response = _authService.Login(userLoginDto);

            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                var loggedInUser = new LoggedInUser()
                {
                    Id = (int)json.SelectToken("userToReturn.id"),
                    FirstName = (string)json.SelectToken("userToReturn.firstName"),
                    LastName = (string)json.SelectToken("userToReturn.lastName"),
                    UserName = (string)json.SelectToken("userToReturn.userName"),
                    Email = (string)json.SelectToken("userToReturn.email"),
                    Pesel = (string)json.SelectToken("userToReturn.pesel"),
                    CreatedDate = (DateTime)json.SelectToken("userToReturn.createdDate"),
                    Token = (string)json.SelectToken("userToReturn.token"),
                    Accounts = json.SelectToken("userToReturn.accounts").ToObject<List<Account>>()
                };

                //open main window
                MainWindow mainWindow = new MainWindow(loggedInUser);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Try again.");
            }
        }

    }
}
