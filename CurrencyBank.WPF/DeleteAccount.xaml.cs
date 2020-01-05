using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;



namespace CurrencyBank.WPF
{
    /// <summary>
    /// Interaction logic for DeleteAccount.xaml
    /// </summary>
    public partial class DeleteAccount : Window
    {
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        public DeleteAccount()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public DeleteAccount(LoggedInUser loggedInUser) : this()
        {
            _loggedInUser = loggedInUser;
            this.SetView();
        }

        private void SetView()
        {
            AccID_cbbx.Items.Clear();
            foreach (var account in _loggedInUser.Accounts)
            {
                string acnt = $"{account.Id} - {account.Currency}";
                AccID_cbbx.Items.Add(acnt);
            }
        }

        private async void DeleteAcc_btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteAcc_btn.IsEnabled = false;

            int id = int.Parse(AccID_cbbx.Text.Where(Char.IsDigit).ToArray());

            var response = await _accountService.DeleteAccount(_loggedInUser.Token, id);

            if (response.IsSuccessStatusCode)
            {
                _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(a => a.Id == id).FirstOrDefault());
                SetView();
                AccID_cbbx.Items.Refresh();
                MessageBox.Show("Konto zostało usunięte");
            }
            else
            {
                var errorMsg = response.Content.ReadAsStringAsync().Result.ToString();
                MessageBoxResult result = MessageBox.Show(
                                                     Properties.Resources.Error_deleteingAccount,
                                                     "Confirmation window",
                                                     MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    WithdrawalWindow withdrawalWindow = new WithdrawalWindow(_loggedInUser);
                    this.Close();
                    withdrawalWindow.Show();
                }
            }
            DeleteAcc_btn.IsEnabled = true;
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInUser);
            mainWindow.Show();
            this.Close();
        }
    }
}
