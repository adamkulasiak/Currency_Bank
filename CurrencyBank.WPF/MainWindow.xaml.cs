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
        private AccountService _accountService;
        private LoggedInUser _loggedInUser;
        
        public MainWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        public MainWindow(LoggedInUser loggedInUser) : this()
        {
            _loggedInUser = loggedInUser;
            SetView();
        }

        public void SetView()
        {
            accountsList.ItemsSource = _loggedInUser.Accounts;
            loggedInAs.Content = $"Logged in as: {_loggedInUser.UserName}";
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void accountsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
