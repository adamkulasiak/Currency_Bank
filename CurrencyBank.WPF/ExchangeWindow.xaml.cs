using CurrencyBank.WPF.Models;
using CurrencyBank.WPF.Services;
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
using System.Linq;

namespace CurrencyBank.WPF
{
    /// <summary>
    /// Interaction logic for ExchangeWindow.xaml
    /// </summary>
    public partial class ExchangeWindow : Window
    {
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        public ExchangeWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }
        public ExchangeWindow(LoggedInUser loggedInUser) : this()
        {
            _loggedInUser = loggedInUser;
            SetView();
        }

        private void SetView()
        {
            var currencies = _loggedInUser.Accounts.Select(x => new { x.Currency, x.Id }).ToList();
            foreach (var item in currencies)
            {
                string value = item.ToString().Substring(1, item.ToString().Length - 2);
                ExchangeFrom_lb.Items.Add(value);
                ExchangeTo_lb.Items.Add(value);
            }
        }

        private async void Exchange_btn_Click(object sender, RoutedEventArgs e)
        {
            int from = int.Parse(ExchangeFrom_lb.Text.Where(Char.IsDigit).ToArray());
            int to = int.Parse(ExchangeTo_lb.Text.Where(Char.IsDigit).ToArray());
            decimal ammount = decimal.Parse(Ammount_txt.Text);

            var response = await _accountService.ExchangeMoney(_loggedInUser.Token, from, to, ammount);

            MessageBox.Show(response.ToString());
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
