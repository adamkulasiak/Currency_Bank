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
    /// Logika interakcji dla klasy AccountOpeningWindow.xaml
    /// </summary>
    public partial class AccountOpeningWindow : Window
    {
        private LoggedInUser _loggedInUser;
        private AccountService _accountService;
        public AccountOpeningWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public AccountOpeningWindow(LoggedInUser loggedInUser) : this()
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            SetView();
        }

        private void SetView()
        {
            foreach (var currency in Enum.GetValues(typeof(Currency)))
            {
                Currency_cbx.Items.Add(currency);
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInUser);
            mainWindow.Show();
            this.Close();
        }

        private async void OpenAcc_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenAcc_btn.IsEnabled = false;

            Enum.TryParse(Currency_cbx.Text, out Currency currency);
            var accountToCreateDto = new AccountToCreateDto
            {
                Currency = currency
            };
            var response = await _accountService.OpenAccount(_loggedInUser.Token, accountToCreateDto);

            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var account = json.ToObject<Account>();
                _loggedInUser.Accounts.Add(account);
            }

            MessageBox.Show("OK");
        }
    }
}
