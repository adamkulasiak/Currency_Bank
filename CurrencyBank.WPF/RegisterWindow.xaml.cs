using CurrencyBank.BLL.Dtos;
using CurrencyBank.BLL.Managers;
using CurrencyBank.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private static readonly HttpClient client = new HttpClient();
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
            Register_btn.IsEnabled = false;
            if(!EmailValidator.IsEmailValid(Email_tb.Text))
            {
                MessageBox.Show("Zly mail");
            }
            else
            {
                MessageBox.Show("Dobry mail");
            }
            var user = new UserRegisterDto()
            {
                FirstName = FirstName_tb.Text,
                LastName = LastName_tb.Text,
                UserName = UserName_tb.Text,
                Email = Email_tb.Text,
                Pesel = Pesel_tb.Text,
                Password = password_tb.Password
            };

            var val = new Dictionary<string, string>
            {
                {"FirstName",user.FirstName },
                {"LastName",user.LastName },
                {"UserName",user.UserName},
                {"Email",user.Email },
                {"Pesel",user.Pesel },
                {"Password",user.Password },
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync("http://localhost:5000/api/auth/register", user).Result;

            MessageBox.Show(response);
            
            if (result is null)
            {
                MessageBox.Show("Error");
                Register_btn.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Success");
                this.CreateRegisterForm();
                Register_btn.IsEnabled = true;
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.Show();
            }
        }
    }
}
