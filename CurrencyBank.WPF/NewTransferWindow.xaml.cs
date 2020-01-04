using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json;
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
    /// Interaction logic for NewTransferWindow.xaml
    /// </summary>
    public partial class NewTransferWindow : Window
    {
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        public NewTransferWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public NewTransferWindow(LoggedInUser loggedInUser): this()
        {
            _loggedInUser = loggedInUser;
            SetView();
        }

        private void SetView()
        {
            var accounts = _loggedInUser.Accounts.ToList();
            foreach (var account in accounts)
            {
                FromAccount_cbbx.Items.Add($"{account.Id} - {account.Currency}");
            }
            
        }

        private async void Transfer_btn_Click(object sender, RoutedEventArgs e)
        {
            Transfer_btn.IsEnabled = false;

            var principalAccountId = int.Parse((FromAccount_cbbx.Text).Where(Char.IsDigit).ToArray());
            var destinationAccountNumber = ToAccount_btn.Text;
            var ammount = decimal.Parse(Ammount_Txt.Text);

            var response = await _accountService.TransferMoney(_loggedInUser.Token, principalAccountId, destinationAccountNumber, ammount);

            if (response.IsSuccessStatusCode)
            {
                JArray jArray = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                var accounts = JsonConvert.DeserializeObject<List<Account>>(jArray.ToString());

                _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(x => x.Id == principalAccountId).FirstOrDefault());
                _loggedInUser.Accounts.Add(accounts[0]);

                if (accounts[1].UserId == _loggedInUser.Id)
                {
                    _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(x => x.AccountNumber == destinationAccountNumber).FirstOrDefault());
                    _loggedInUser.Accounts.Add(accounts[1]);
                }

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
