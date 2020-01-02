using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
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
            foreach (var account in _loggedInUser.Accounts)
            {
                string acnt = $"{account.Id} - {account.Currency}";
                AccID_cbbx.Items.Add(acnt);
            }
        }

        private async void DeleteAcc_btn_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(AccID_cbbx.Text.Where(Char.IsDigit).ToArray());

            var response = await _accountService.DeleteAccount(_loggedInUser.Token, id);

            if (response.IsSuccessStatusCode)
            {
                _loggedInUser.Accounts.Remove(_loggedInUser.Accounts.Where(a => a.Id == id).FirstOrDefault());
                MessageBox.Show("Konto zostało usunięte");
            }
            else
            {
                MessageBox.Show("Błąd przy usuwaniu");
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
