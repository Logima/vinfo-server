namespace VitalInfo
{
    partial class OptionsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxNetworkInterface = new System.Windows.Forms.ListBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPhoneIp = new System.Windows.Forms.TextBox();
            this.textBoxPhonePort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMyipHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMyipPort = new System.Windows.Forms.TextBox();
            this.textBoxMyipUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxNpFile = new System.Windows.Forms.TextBox();
            this.checkBoxSendNp = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMyipInterval = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network inteface";
            // 
            // listBoxNetworkInterface
            // 
            this.listBoxNetworkInterface.FormattingEnabled = true;
            this.listBoxNetworkInterface.Location = new System.Drawing.Point(12, 25);
            this.listBoxNetworkInterface.Name = "listBoxNetworkInterface";
            this.listBoxNetworkInterface.Size = new System.Drawing.Size(421, 69);
            this.listBoxNetworkInterface.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(359, 216);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(278, 216);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Phone IP";
            // 
            // textBoxPhoneIp
            // 
            this.textBoxPhoneIp.Location = new System.Drawing.Point(12, 113);
            this.textBoxPhoneIp.Name = "textBoxPhoneIp";
            this.textBoxPhoneIp.Size = new System.Drawing.Size(167, 20);
            this.textBoxPhoneIp.TabIndex = 5;
            // 
            // textBoxPhonePort
            // 
            this.textBoxPhonePort.Location = new System.Drawing.Point(185, 113);
            this.textBoxPhonePort.Name = "textBoxPhonePort";
            this.textBoxPhonePort.Size = new System.Drawing.Size(47, 20);
            this.textBoxPhonePort.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "MyIP Host";
            // 
            // textBoxMyipHost
            // 
            this.textBoxMyipHost.Location = new System.Drawing.Point(12, 152);
            this.textBoxMyipHost.Name = "textBoxMyipHost";
            this.textBoxMyipHost.Size = new System.Drawing.Size(167, 20);
            this.textBoxMyipHost.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Port";
            // 
            // textBoxMyipPort
            // 
            this.textBoxMyipPort.Location = new System.Drawing.Point(185, 152);
            this.textBoxMyipPort.Name = "textBoxMyipPort";
            this.textBoxMyipPort.Size = new System.Drawing.Size(47, 20);
            this.textBoxMyipPort.TabIndex = 11;
            // 
            // textBoxMyipUrl
            // 
            this.textBoxMyipUrl.Location = new System.Drawing.Point(238, 152);
            this.textBoxMyipUrl.Name = "textBoxMyipUrl";
            this.textBoxMyipUrl.Size = new System.Drawing.Size(195, 20);
            this.textBoxMyipUrl.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(238, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "URL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "File containing Now Playing -info";
            // 
            // textBoxNpFile
            // 
            this.textBoxNpFile.Location = new System.Drawing.Point(12, 191);
            this.textBoxNpFile.Name = "textBoxNpFile";
            this.textBoxNpFile.Size = new System.Drawing.Size(279, 20);
            this.textBoxNpFile.TabIndex = 15;
            this.textBoxNpFile.Click += new System.EventHandler(this.TextBoxNpFileClick);
            // 
            // checkBoxSendNp
            // 
            this.checkBoxSendNp.AutoSize = true;
            this.checkBoxSendNp.Checked = true;
            this.checkBoxSendNp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendNp.Location = new System.Drawing.Point(297, 193);
            this.checkBoxSendNp.Name = "checkBoxSendNp";
            this.checkBoxSendNp.Size = new System.Drawing.Size(136, 17);
            this.checkBoxSendNp.TabIndex = 17;
            this.checkBoxSendNp.Text = "Send Now Playing -info";
            this.checkBoxSendNp.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "IP fetch interval (minutes)";
            // 
            // textBoxMyipInterval
            // 
            this.textBoxMyipInterval.Location = new System.Drawing.Point(386, 113);
            this.textBoxMyipInterval.Name = "textBoxMyipInterval";
            this.textBoxMyipInterval.Size = new System.Drawing.Size(47, 20);
            this.textBoxMyipInterval.TabIndex = 7;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(446, 250);
            this.Controls.Add(this.textBoxMyipInterval);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBoxSendNp);
            this.Controls.Add(this.textBoxNpFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxMyipUrl);
            this.Controls.Add(this.textBoxMyipPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMyipHost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPhonePort);
            this.Controls.Add(this.textBoxPhoneIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listBoxNetworkInterface);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.VisibleChanged += new System.EventHandler(this.OptionsFormVisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxNetworkInterface;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPhoneIp;
        private System.Windows.Forms.TextBox textBoxPhonePort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMyipHost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMyipPort;
        private System.Windows.Forms.TextBox textBoxMyipUrl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBoxNpFile;
        private System.Windows.Forms.CheckBox checkBoxSendNp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMyipInterval;
    }
}