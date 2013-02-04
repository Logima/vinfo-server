using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace VitalInfo
{
    class Listener
    {
        private readonly Options _options;
        private readonly MainForm _mainForm;

        private Thread _thread;
        private volatile bool _continue = true;

        private UdpClient _udpClient;

        public Listener(ref Options options, MainForm mainForm)
        {
            _options = options;
            _mainForm = mainForm;
        }

        public void Start()
        {
            _continue = true;
            _thread = new Thread(DoLoop) { Name = "ListenerLoop" };
            _thread.Start();
            _mainForm.LogLine("Listenerthread started.");
        }

        public void Stop()
        {
            _continue = false;
            _udpClient.Close();
            _thread.Join();
            _mainForm.LogLine("Listenerthread stopped.");
        }

        private void DoLoop()
        {
            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 25463));
            var sender = new IPEndPoint(IPAddress.Parse(_options.PhoneIp), 0);
            while (_continue)
            {
                Thread.Sleep(10);
                try
                {
                    var received = _udpClient.Receive(ref sender);
                    if (received.Length == 0) continue;
                    ParseReceivedData(Encoding.UTF8.GetString(received, 0, received.Length));
                }
                catch (Exception e)
                {
                    if (_continue) _mainForm.LogLine("Warning: Listenerthread: " + e);
                }
            }
        }

        private void ParseReceivedData(string receivedData)
        {
            _mainForm.LogLine("Listener received: " + receivedData.Trim());
        }
    }
}
