using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyBank.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (!File.Exists(@"C:\Database\lang.txt"))
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Database\lang.txt"))
                {
                    sw.WriteLine("pl-PL");
                }
            }
                string lang = "";
            var lines = File.ReadAllLines(@"C:\Database\lang.txt");
            for (var i = 0; i < lines.Length; i++)
            {
                lang = lines[i];
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }
    }
}
