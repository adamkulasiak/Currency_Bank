using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        public Form1()
        {
            InitializeComponent();
        }

        private string RunScript(string script)
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            //txtOutput.Clear();
            //txtOutput.Text = RunScript(txtInput.Text);

            string exe = @"C:\Users\Adam\Desktop\Currency_Bank\WebApiCurrencyBank\serviceManagement\installService.bat";
            var psi = new ProcessStartInfo();
            psi.CreateNoWindow = true; //This hides the dos-style black window that the command prompt usually shows
            psi.FileName = @"cmd.exe";
            psi.Verb = "runas"; //This is what actually runs the command as administrator
            psi.Arguments = "/C " + exe;
            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception)
            {
                //If you are here the user clicked decline to grant admin privileges (or he's not administrator)
            }
        }
    }
}
