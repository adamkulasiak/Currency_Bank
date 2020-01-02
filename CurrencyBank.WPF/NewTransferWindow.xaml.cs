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

        private void Transfer_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
