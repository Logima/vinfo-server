using System;
using System.Linq;
using System.Windows.Forms;

namespace VitalInfo
{
    public partial class MainForm : Form
    {
        private const Int32 WM_DEVICECHANGE = 0x219;
        private const Int32 DBT_DEVICEARRIVAL = 0x8000;
        private const Int32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

        private readonly Options _options;
        private readonly LoopThread _loopThread;
        private readonly Listener _listener;
        public delegate void InvokeDelegate(string s, bool debug);

        private readonly OptionsForm _optionsForm;
        private NotifyIcon _trayIcon = new NotifyIcon();

        public MainForm()
        {
            InitializeComponent();
            _trayIcon.Visible = false;
            _trayIcon.MouseClick += TrayIconMouseClick;
            _trayIcon.Icon = Icon;
            _trayIcon.Text = Text;

            _options = Options.LoadFromFile(this);
            _options.PrintDebug = checkBoxDebug.Checked;
            _loopThread = new LoopThread(ref _options, this);
            _listener = new Listener(ref _options, this);

            _optionsForm = new OptionsForm(ref _options, ref _loopThread, this);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_DEVICECHANGE) return;
            var wParam = m.WParam.ToInt32();
            if (wParam == DBT_DEVICEARRIVAL || wParam == DBT_DEVICEREMOVECOMPLETE)
            {
                _loopThread.RescanDrives = true;
            }
        }

        public void LogLine(String s, bool debug = false)
        {
            if (_options != null && !_options.PrintDebug && debug)
            {
                return;
            }
            if (logBox.InvokeRequired)
            {
                var d = new InvokeDelegate(LogLine);
                Invoke(d, new object[] { s, debug });
                return;
            }
            if (debug) s = "Debug: " + s;

            if (logBox.Lines.Length > 100)
            {
                logBox.Lines = logBox.Lines.Skip(logBox.Lines.Length - 100).ToArray();
            }

            logBox.AppendText((logBox.Text.Length > 0 ? Environment.NewLine : "") + DateTime.Now.ToString("d.M.yy HH:mm:ss: ") + s);
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonStartStop.Text.Equals("Start")) return;
            LogLine("Waiting for background threads to stop...");
            Application.DoEvents();
            _loopThread.Stop();
            _listener.Stop();
        }

        private void CheckBoxDebugCheckedChanged(object sender, EventArgs e)
        {
            _options.PrintDebug = checkBoxDebug.Checked;
        }

        private void ButtonOptionsClick(object sender, EventArgs e)
        {
            _optionsForm.ShowDialog();
        }

        private void ButtonStartStopClick(object sender, EventArgs e)
        {
            if (buttonStartStop.Text.Equals("Start"))
            {
                _loopThread.Start();
                _listener.Start();
                buttonStartStop.Text = "Stop";
            }
            else
            {
                LogLine("Waiting for background threads to stop...");
                Application.DoEvents();
                _loopThread.Stop();
                _listener.Stop();
                buttonStartStop.Text = "Start";
            }
        }

        private void MainFormResize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            // if the form has been minimised
            Hide();
            _trayIcon.Visible = true;
        }

        void TrayIconMouseClick(object sender, MouseEventArgs e)
        {
            Show();
            _trayIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}
