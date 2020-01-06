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
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallerAPI
{
    public partial class Form1 : Form
    {
        private string _filePath;
        private string _filePathDotnetCore;
        private string _filePathDb;
        public Form1()
        {
            InitializeComponent();
            FillCombobox();

            CreateDirectory(@"C:\temp");

            using (StreamWriter writetext = new StreamWriter(@"C:\temp\uninstallCurrrencyBank.bat"))
            {
                writetext.WriteLine($"sc stop CurrencyBankAPI");
                writetext.WriteLine("sc delete CurrencyBankAPI");
            }
        }

        private void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private void FillCombobox()
        {
            cbxLanguage.ValueMember ="Name";
            cbxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;

            cbxLanguage.Items.Add(new Item(1, "pl-PL"));
            cbxLanguage.Items.Add(new Item(2, "en"));
        }

        private class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Item(int id, string name)
            {
                Id = id;
                Name = name;
            }
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
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(@"C:\temp\installCurrrencyBank.bat");
                File.Delete(@"C:\temp\startService.bat");
                File.Delete(@"C:\temp\stopService.bat");
                File.Delete(@"C:\temp\uninstallCurrrencyBank.bat");
            }
            catch (Exception)
            {

            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePath = openFileDialog.FileName;
            if (_filePath.Length > 0)
            {
                txtOutput.AppendText("Wybrany plik: "+ _filePath + Environment.NewLine);
                CreateDirectory(@"C:\temp");
                
                using (StreamWriter writetext = new StreamWriter(@"C:\temp\installCurrrencyBank.bat"))
                {
                    writetext.WriteLine($"sc create CurrencyBankAPI binPath={_filePath}");
                    writetext.WriteLine("sc start CurrencyBankAPI");
                }
                using (StreamWriter writetext = new StreamWriter(@"C:\temp\startService.bat"))
                {
                    writetext.WriteLine("sc start CurrencyBankAPI");
                }
                using (StreamWriter writetext = new StreamWriter(@"C:\temp\stopService.bat"))
                {
                    writetext.WriteLine("sc stop CurrencyBankAPI");
                }
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var result = RunScript(@"C:\temp\installCurrrencyBank.bat");

            CreateDirectory(@"C:\temp");
            using (StreamWriter writetext = new StreamWriter(@"C:\temp\uninstallCurrrencyBank.bat"))
            {
                writetext.WriteLine($"sc stop CurrencyBankAPI");
                writetext.WriteLine("sc delete CurrencyBankAPI");
            }
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            var result = RunScript(@"C:\temp\uninstallCurrrencyBank.bat");
        }

        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            string command = "Get-Service -Displayname \"CurrencyBankAPI\"";
            txtPSOutput.Clear();
            txtPSOutput.Text = RunPowershellScript(command);
            if (string.IsNullOrEmpty(txtPSOutput.Text))
            {
                txtPSOutput.Text = "Usługa nie jest zainstalowana";
            }
        }

        private string RunPowershellScript(string script)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(script);
            pipeline.Commands.Add("Out-String");
            Collection<PSObject> results = pipeline.Invoke();
            runspace.Close();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject psObject in results)
            {
                stringBuilder.Append(psObject.ToString());
            }
            return stringBuilder.ToString();
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            RunScript(@"C:\temp\startService.bat");
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            RunScript(@"C:\temp\stopService.bat");
        }

        private void btnDotnetCoreCheck_Click(object sender, EventArgs e)
        {
            string command = @"dotnet --info";
            txtPSOutput.Clear();
            txtPSOutput.Text = RunPowershellScript(command);
            if (string.IsNullOrEmpty(txtPSOutput.Text))
            {
                txtPSOutput.Text = "Brak zainstalowanych pakietów dotnet core";
            }
        }

        private void btn_installRuntime_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePathDotnetCore = openFileDialog.FileName;

            if (_filePathDotnetCore.Length > 0)
            {
                string command = $"Start-Process -Wait -FilePath \"{_filePathDotnetCore}\" -ArgumentList \" / S / v / qn\" -passthru";
                txtPSOutput.Clear();
                txtPSOutput.Text = RunPowershellScript(command);
            }
        }

        private void btnDbCopy_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePathDb = openFileDialog.FileName;

            if (_filePathDb.Length > 0)
            {
                try
                {
                    CreateDirectory(@"C:\Database");
                    if (File.Exists(@"C:\Database\CurrencyBankDb.db"))
                    {
                        File.Delete(@"C:\Database\CurrencyBankDb.db");
                    }
                    File.Copy(_filePathDb, @"C:\Database\CurrencyBankDb.db");
                    txtOutput.AppendText("Baza danych skopiowana pomyślnie" + Environment.NewLine);
                }
                catch (Exception err)
                {
                    txtOutput.AppendText("Błąd podczas kopiowania bazy danych" + Environment.NewLine);
                    txtOutput.AppendText(err.ToString());
                }
            }
        }

        private void btnSetLanguage_Click(object sender, EventArgs e)
        {
            string path = @"C:\Database";

            CreateDirectory(path);

            try
            {
                
                using (StreamWriter sw = new StreamWriter(@"C:\Database\lang.txt"))
                {
                    sw.WriteLine(cbxLanguage.Text);
                }
                txtOutput.AppendText("Język dla aplikacji WPF został ustawiony pomyślnie" + Environment.NewLine);
            }
            catch (Exception ) { txtOutput.AppendText("Błąd podczas ustawiania języka" + Environment.NewLine); return; }
        }

    }
}
