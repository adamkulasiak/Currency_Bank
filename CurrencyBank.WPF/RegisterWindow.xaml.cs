using CurrencyBank.BLL.Dtos;
using CurrencyBank.BLL.Managers;
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

        private void CreateRegisterForm()
        {
            FirstName_tb.Text = "";
            LastName_tb.Text = "";
            UserName_tb.Text = "";
            Email_tb.Text = "";
            Pesel_tb.Text = "";
            password_tb.Password = "";
        }

        private async void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            var user = new UserRegisterDto()
            {
                FirstName = FirstName_tb.Text,
                LastName = LastName_tb.Text,
                UserName = UserName_tb.Text,
                Email = Email_tb.Text,
                Pesel = Pesel_tb.Text,
                Password = password_tb.Password
        };

            var result = await _authManager.Register(user, user.Password);

            if (result is null)
                MessageBox.Show("Error");
            else
            {
                MessageBox.Show("Success");
                this.CreateRegisterForm();
                this.IsEnabled = true;
            }
        }
    }
}
