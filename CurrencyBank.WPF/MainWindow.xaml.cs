using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyBank.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoggedInUser _loggedInUser;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(LoggedInUser loggedInUser) : this()
        {
            _loggedInUser = loggedInUser;
            SetView();
        }

        public void SetView()
        {
            accountsList.ItemsSource = _loggedInUser.Accounts;
            loggedInAs.Content += $": {_loggedInUser.UserName}";
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void accountsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            AccountOpeningWindow accountOpeningWindow = new AccountOpeningWindow(_loggedInUser);
            this.Close();
            accountOpeningWindow.Show();
        }

        private void TransferMoney_btn_Click(object sender, RoutedEventArgs e)
        {
            NewTransferWindow newTransferWindow = new NewTransferWindow(_loggedInUser);
            this.Close();
            newTransferWindow.Show();
        }

        private void CashOut_btn_Click(object sender, RoutedEventArgs e)
        {
            WithdrawalWindow withdrawalWindow = new WithdrawalWindow(_loggedInUser);
            this.Close();
            withdrawalWindow.Show();
        }

        private void Exchange_btn_Click(object sender, RoutedEventArgs e)
        {
            ExchangeWindow exchangeWindow = new ExchangeWindow(_loggedInUser);
            this.Close();
            exchangeWindow.Show();
        }

        private void CashIn_btn_Click(object sender, RoutedEventArgs e)
        {
            DepositCashIn depositCashIn = new DepositCashIn(_loggedInUser);
            this.Close();
            depositCashIn.Show();
        }

        private void DeleteAccount_btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccount deleteAccount = new DeleteAccount(_loggedInUser);
            this.Close();
            deleteAccount.Show();
        }

        private void LoggOff_btn_Click(object sender, RoutedEventArgs e)
        {
            _loggedInUser.Dispose();
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
