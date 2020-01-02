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
    /// Interaction logic for WithdrawalWindow.xaml
    /// </summary>
    public partial class WithdrawalWindow : Window
    {
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        public WithdrawalWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public WithdrawalWindow(LoggedInUser loggedInUser) : this()
        {
            _loggedInUser = loggedInUser;
        }

        private void AccountID_btn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Withdraw_btn_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(AccountID_txt.Text);
            var ammount = decimal.Parse(Amount_txt.Text);

            var response = await _accountService.Withdrawal(_loggedInUser.Token, id, ammount);

            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var account = _loggedInUser.Accounts.Where(x => x.Id == id).FirstOrDefault();
                account.Balance = (decimal)json.SelectToken("balance");
                this.Close();
                MessageBox.Show($"You have {account.Balance}");
            }
        }
    }
}
