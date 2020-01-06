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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            Exchange_btn.IsEnabled = false;
            try
            {
                int from = int.Parse(ExchangeFrom_lb.Text.Where(Char.IsDigit).ToArray());
                int to = int.Parse(ExchangeTo_lb.Text.Where(Char.IsDigit).ToArray());
                decimal ammount = decimal.Parse(Ammount_txt.Text);

                var response = await _accountService.ExchangeMoney(_loggedInUser.Token, from, to, ammount);

                if (response.IsSuccessStatusCode)
                {
                    JArray jArray = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    var accounts = JsonConvert.DeserializeObject<List<Account>>(jArray.ToString());

                    var accToRemove = _loggedInUser.Accounts.Where(x => x.Id == from || x.Id == to).ToList();
                    foreach (var acc in accToRemove)
                    {
                        _loggedInUser.Accounts.Remove(acc);
                    }

                    foreach (var acc in accounts)
                    {
                        _loggedInUser.Accounts.Add(acc);
                    }

                    MessageBox.Show("Pomyślnie wymieniono");
                }
                else
                {
                    MessageBox.Show("Błąd podczas wymiany");
                    Exchange_btn.IsEnabled = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wybierz wartości z list");
                Exchange_btn.IsEnabled = true;
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
