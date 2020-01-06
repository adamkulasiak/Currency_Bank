namespace InstallerAPI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnInstall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChoose = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.txtPSOutput = new System.Windows.Forms.TextBox();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnStopService = new System.Windows.Forms.Button();
            this.btnDotnetCoreCheck = new System.Windows.Forms.Button();
            this.btn_installRuntime = new System.Windows.Forms.Button();
            this.btnDbCopy = new System.Windows.Forms.Button();
            this.cbxLanguage = new System.Windows.Forms.ComboBox();
            this.btnSetLanguage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(181, 18);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "Instaluj";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wybierz usługę";
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(100, 18);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 3;
            this.btnChoose.Text = "Wybierz";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(16, 65);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(652, 110);
            this.txtOutput.TabIndex = 4;
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(262, 18);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnUninstall.TabIndex = 5;
            this.btnUninstall.Text = "Odinstaluj";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // txtPSOutput
            // 
            this.txtPSOutput.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txtPSOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPSOutput.Location = new System.Drawing.Point(16, 234);
            this.txtPSOutput.Multiline = true;
            this.txtPSOutput.Name = "txtPSOutput";
            this.txtPSOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPSOutput.Size = new System.Drawing.Size(652, 204);
            this.txtPSOutput.TabIndex = 6;
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.Location = new System.Drawing.Point(15, 205);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(94, 23);
            this.btnCheckStatus.TabIndex = 7;
            this.btnCheckStatus.Text = "Sprawdź status";
            this.btnCheckStatus.UseVisualStyleBackColor = true;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(115, 205);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(75, 23);
            this.btnStartService.TabIndex = 8;
            this.btnStartService.Text = "Start";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(196, 205);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(75, 23);
            this.btnStopService.TabIndex = 9;
            this.btnStopService.Text = "Stop";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // btnDotnetCoreCheck
            // 
            this.btnDotnetCoreCheck.Location = new System.Drawing.Point(277, 205);
            this.btnDotnetCoreCheck.Name = "btnDotnetCoreCheck";
            this.btnDotnetCoreCheck.Size = new System.Drawing.Size(227, 23);
            this.btnDotnetCoreCheck.TabIndex = 10;
            this.btnDotnetCoreCheck.Text = "Sprawdź zainstalowane wersje .NET Core";
            this.btnDotnetCoreCheck.UseVisualStyleBackColor = true;
            this.btnDotnetCoreCheck.Click += new System.EventHandler(this.btnDotnetCoreCheck_Click);
            // 
            // btn_installRuntime
            // 
            this.btn_installRuntime.Location = new System.Drawing.Point(511, 205);
            this.btn_installRuntime.Name = "btn_installRuntime";
            this.btn_installRuntime.Size = new System.Drawing.Size(120, 23);
            this.btn_installRuntime.TabIndex = 11;
            this.btn_installRuntime.Text = "Zainstaluj .NET Core";
            this.btn_installRuntime.UseVisualStyleBackColor = true;
            this.btn_installRuntime.Click += new System.EventHandler(this.btn_installRuntime_Click);
            // 
            // btnDbCopy
            // 
            this.btnDbCopy.Location = new System.Drawing.Point(343, 18);
            this.btnDbCopy.Name = "btnDbCopy";
            this.btnDbCopy.Size = new System.Drawing.Size(113, 23);
            this.btnDbCopy.TabIndex = 12;
            this.btnDbCopy.Text = "Kopiuj baze danych";
            this.btnDbCopy.UseVisualStyleBackColor = true;
            this.btnDbCopy.Click += new System.EventHandler(this.btnDbCopy_Click);
            // 
            // cbxLanguage
            // 
            this.cbxLanguage.FormattingEnabled = true;
            this.cbxLanguage.Location = new System.Drawing.Point(475, 20);
            this.cbxLanguage.Name = "cbxLanguage";
            this.cbxLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbxLanguage.TabIndex = 13;
            // 
            // btnSetLanguage
            // 
            this.btnSetLanguage.Location = new System.Drawing.Point(602, 20);
            this.btnSetLanguage.Name = "btnSetLanguage";
            this.btnSetLanguage.Size = new System.Drawing.Size(75, 23);
            this.btnSetLanguage.TabIndex = 14;
            this.btnSetLanguage.Text = "Ustaw język";
            this.btnSetLanguage.UseVisualStyleBackColor = true;
            this.btnSetLanguage.Click += new System.EventHandler(this.btnSetLanguage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 450);
            this.Controls.Add(this.btnSetLanguage);
            this.Controls.Add(this.cbxLanguage);
            this.Controls.Add(this.btnDbCopy);
            this.Controls.Add(this.btn_installRuntime);
            this.Controls.Add(this.btnDotnetCoreCheck);
            this.Controls.Add(this.btnStopService);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.btnCheckStatus);
            this.Controls.Add(this.txtPSOutput);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Instalator usługi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.TextBox txtPSOutput;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnStopService;
        private System.Windows.Forms.Button btnDotnetCoreCheck;
        private System.Windows.Forms.Button btn_installRuntime;
        private System.Windows.Forms.Button btnDbCopy;
        private System.Windows.Forms.ComboBox cbxLanguage;
        private System.Windows.Forms.Button btnSetLanguage;
    }
}

