using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            SetView();
        }

        private void SetView()
        {
            var accounts = _loggedInUser.Accounts.ToList();
            foreach (var account in accounts)
            {
                AccountID_cbx.Items.Add($"{account.Id} - {account.Currency}");
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInUser);
            mainWindow.Show();
            this.Close();
        }

        private async void Withdraw_btn_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((AccountID_cbx.Text).Where(Char.IsDigit).ToArray());
            var ammount = decimal.Parse(Amount_txt.Text.Replace(" ", string.Empty));

            var response = await _accountService.Withdrawal(_loggedInUser.Token, id, ammount);

            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var account = json.ToObject<Account>();
                _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(a => a.Id == id).FirstOrDefault());
                _loggedInUser.Accounts.Add(account);
                MessageBox.Show(Properties.Resources.WithdrawedSuccessfully_msg + $"{account.Balance}");
            }
            else
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result.ToString());
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
