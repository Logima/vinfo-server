using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace VitalInfo
{
    public partial class OptionsForm : Form
    {
        private readonly Options _options;
        private readonly MainForm _mainForm;
        private readonly LoopThread _loopThread;

        public OptionsForm(ref Options options, ref LoopThread loopThread, MainForm mainForm)
        {
            InitializeComponent();

            _options = options;
            _loopThread = loopThread;
            _mainForm = mainForm;
            OptionsFormVisibleChanged(null, null);
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Hide();
        }

        private void OptionsFormVisibleChanged(object sender, EventArgs e)
        {
            if (Visible == false)
            {
                return;
            }


            textBoxPhoneIp.Text = _options.PhoneIp;
            textBoxPhonePort.Text = _options.PhonePort.ToString();
            textBoxMyipHost.Text = _options.MyipHost;
            textBoxMyipPort.Text = _options.MyipPort.ToString();
            textBoxMyipUrl.Text = _options.MyipUrl;
            textBoxMyipInterval.Text = _options.MyipInterval.ToString();
            textBoxNpFile.Text = _options.NpFile;
            checkBoxSendNp.Checked = _options.SendNp;


            // set listBoxNetworkInterface
            listBoxNetworkInterface.Items.Clear();
            var instanceNames = new PerformanceCounterCategory("Network Interface").GetInstanceNames();
            listBoxNetworkInterface.Items.AddRange(instanceNames);
            if (instanceNames.Length > 0)
            {
                listBoxNetworkInterface.SelectedIndex = 0;
            }
            if (instanceNames.Length > _options.NetIndex)
            {
                listBoxNetworkInterface.SelectedIndex = _options.NetIndex;
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            _options.PhoneIp = textBoxPhoneIp.Text;
            _options.PhonePort = Convert.ToInt32(textBoxPhonePort.Text);
            _options.MyipHost = textBoxMyipHost.Text;
            _options.MyipPort = Convert.ToInt32(textBoxMyipPort.Text);
            _options.MyipUrl = textBoxMyipUrl.Text;
            _options.MyipInterval = Convert.ToInt32(textBoxMyipInterval.Text);
            _options.NpFile = textBoxNpFile.Text;
            _options.SendNp = checkBoxSendNp.Checked;
            _options.NetIndex = listBoxNetworkInterface.SelectedIndex;

            _loopThread.CreateNetworkCounters();

            var err = _options.SaveToFile();
            if (err != null) _mainForm.LogLine("Warning: Failed to write settings to file: " + err);

            Hide();
        }

        private void TextBoxNpFileClick(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            textBoxNpFile.Text = openFileDialog1.FileName;
        }
    }
}
