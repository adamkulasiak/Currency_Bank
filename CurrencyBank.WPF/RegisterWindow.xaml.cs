using CurrencyBank.BLL.Managers;
using Database.Models;
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

namespace CurrencyBank.WPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly AuthManager _authManager;
        public RegisterWindow()
        {
            InitializeComponent();
            _authManager = new AuthManager();
        }

        private async void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            var user = new User()
            {
                FirstName = FirstName_tb.Text,
                LastName = LastName_tb.Text,
                UserName = UserName_tb.Text,
                Email = Email_tb.Text,
                Pesel = Pesel_tb.Text
            };

            var password = password_tb.Password;

            var result = await _authManager.Register(user, password);

            if (result is null)
                MessageBox.Show("Error");
            else MessageBox.Show("Success");
        }
    }
}
