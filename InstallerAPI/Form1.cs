using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallerAPI
{
    public partial class Form1 : Form
    {
        private string _filePath;
        public Form1()
        {
            InitializeComponent();
        }

        private bool RunScript(string script)
        {
            string exe = script;
            var psi = new ProcessStartInfo();
            psi.CreateNoWindow = false;
            psi.FileName = @"cmd.exe";
            psi.Verb = "runas";
            psi.Arguments = "/C " + exe;
            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                File.Delete(script);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePath = openFileDialog.FileName;
            if (_filePath.Length > 0)
            {
                txtOutput.AppendText("Wybrany plik: "+ _filePath + Environment.NewLine);
                if (!Directory.Exists(@"C:\temp"))
                {
                    Directory.CreateDirectory(@"C:\temp");
                }
                using (StreamWriter writetext = new StreamWriter(@"C:\temp\installCurrrencyBank.bat"))
                {
                    writetext.WriteLine($"sc create CurrencyBankAPI binPath={_filePath}");
                    writetext.WriteLine("sc start CurrencyBankAPI");
                    writetext.WriteLine("pause");
                }
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var result = RunScript(@"C:\temp\installCurrrencyBank.bat");

            if (!Directory.Exists(@"C:\temp"))
            {
                Directory.CreateDirectory(@"C:\temp");
            }
            using (StreamWriter writetext = new StreamWriter(@"C:\temp\uninstallCurrrencyBank.bat"))
            {
                writetext.WriteLine($"sc stop CurrencyBankAPI");
                writetext.WriteLine("sc delete CurrencyBankAPI");
                writetext.WriteLine("pause");
            }
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            var result = RunScript(@"C:\temp\uninstallCurrrencyBank.bat");
        }
    }
}
