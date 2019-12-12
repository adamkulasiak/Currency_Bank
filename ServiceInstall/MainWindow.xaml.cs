using Microsoft.Win32;
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

namespace ServiceInstall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePath = openFileDialog.FileName;
            if (_filePath.Length > 0)
            {
                CreateService();
            }
        }

        private void CreateService()
        {
            string strCmdText = $"sc create \"CurrencyBankAPI\" binPath=\"{_filePath}\" sc start CurrencyBankAPI";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}
