using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for DepositCashIn.xaml
    /// </summary>
    public partial class DepositCashIn : Window
    {
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        public DepositCashIn()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public DepositCashIn(LoggedInUser loggedInUser) :this()
        {
            _loggedInUser = loggedInUser;
            SetView();
        }

        private void SetView()
        {
            var accounts = _loggedInUser.Accounts.Select(x => new { x.Currency, x.Id }).ToList();
            foreach (var account in accounts)
            {
                AccountID_cbx.Items.Add($"{account.Id} - {account.Currency}");
            }
        }

        private async void CashIn_btn_Click(object sender, RoutedEventArgs e)
        {
            CashIn_btn.IsEnabled = false;

            var accountId = int.Parse((AccountID_cbx.Text).Where(Char.IsDigit).ToArray());
            var ammount = decimal.Parse(Amount_txt.Text);

            var response = await _accountService.CashInMoney(_loggedInUser.Token, accountId, ammount);

            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var account = json.ToObject<Account>();

                _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(a => a.Id == accountId).FirstOrDefault());
                _loggedInUser.Accounts.Add(account);

                MessageBox.Show("OK");
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInUser);
            mainWindow.Show();
            this.Close();
        }
    }
}
