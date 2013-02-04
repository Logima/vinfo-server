using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace VitalInfo
{
    public class LoopThread
    {
        private Thread _thread;
        private volatile bool _continue = true;
        private readonly MainForm _mainForm;
        private readonly Options _options;

        public volatile bool RescanDrives = false;

        private static String _localIp = "0.0.0.0", _remoteIp = "0.0.0.0";
        private readonly BackgroundWorker _getIpsBw = new BackgroundWorker();
        private UdpClient _conn;
        private PerformanceCounter networkInCounter, networkOutCounter;

        public LoopThread(ref Options options, MainForm mainForm)
        {
            _options = options;
            _mainForm = mainForm;
        }


        public void Start()
        {
            _continue = true;
            _thread = new Thread(DoLoop) { Name = "VitalInfoLoop" };
            _thread.Start();
            _mainForm.LogLine("Background thread started.");
        }

        public void Stop()
        {
            _continue = false;
            _thread.Join();
            _mainForm.LogLine("Background thread stopped.");
        }

        public void CreateNetworkCounters()
        {
            var instanceNames = new PerformanceCounterCategory("Network Interface").GetInstanceNames();
            networkInCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", instanceNames[_options.NetIndex]);
            networkOutCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instanceNames[_options.NetIndex]);
        }

        private void DoLoop()
        {
            _conn = new UdpClient(_options.PhoneIp, _options.PhonePort);

            var ramFreeCounter = new PerformanceCounter("Memory", "Available MBytes");
            var computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            var totalRam = computerInfo.TotalPhysicalMemory / 1024 / 1024;

            CreateNetworkCounters();
            
            var pc = new PerformanceCounter("Processor", "% Processor Time");
            var instances = new PerformanceCounterCategory("Processor").GetInstanceNames();
            var cs = new Dictionary<string, CounterSample>();

            foreach (var s in instances)
            {
                pc.InstanceName = s;
                cs.Add(s, pc.NextSample());
            }
            Thread.Sleep(999);

            var drives = DriveInfo.GetDrives();

            var volume = new Volume();

            _getIpsBw.DoWork += GetIps;
            //_getIpsBw.RunWorkerAsync(true);

            var myipTimer = new Timer(StartBw, null, TimeSpan.Zero, TimeSpan.FromMinutes(_options.MyipInterval));


            while (_continue)
            {
                var startTime = DateTime.Now;

                var strout = ".start.";
                foreach (var s in instances)
                {
                    pc.InstanceName = s;
                    var ns = pc.NextSample();
                    if (s.Equals("_Total")) strout += s + "|" + Math.Round(CounterSample.Calculate(cs[s], ns), 2) + "\n";
                    else strout += s + "|" + Math.Round(CounterSample.Calculate(cs[s], ns), 0) + "\n";
                    cs[s] = ns;
                }
                strout += "MemUsed|" + (totalRam - ramFreeCounter.NextValue()) + "\n";
                strout += "MemTotal|" + totalRam + "\n";

                try
                {
                    strout += "NetIn|" + networkInCounter.NextValue() + "\n";
                    strout += "NetOut|" + networkOutCounter.NextValue() + "\n";
                }
                catch (Exception e)
                {
                    _mainForm.LogLine("Warning: Failed to get network activity: " + e);
                    _mainForm.LogLine("Resetting to first adapter.");
                    _options.NetIndex = 0;
                    CreateNetworkCounters();
                }
                strout += "LocalIp|" + _localIp + "\n";
                strout += "RemoteIp|" + _remoteIp + "\n";

                if (RescanDrives)
                {
                    _mainForm.LogLine("Drivechange detected, rescanning drives.", true);
                    drives = DriveInfo.GetDrives();
                    RescanDrives = false;
                }
                foreach (var drive in drives.Where(drive => drive.DriveType == DriveType.Fixed))
                {
                    try
                    {
                        strout += "DriveInfo|" + drive.Name + "|" + drive.AvailableFreeSpace + "\n";
                    }
                    catch
                    {

                    }
                }

                strout += "Np|" + GetNp() + "\n";

                var muteStatus = volume.GetMute();
                var vol = volume.GetVol();

                strout += "mute|" + (muteStatus ? "1" : "0") + "\n";
                strout += "vol|" + vol + "\n";

                SendInfo(strout);


                var duration = DateTime.Now - startTime;
                if (duration.Milliseconds > 1000)
                {
                    _mainForm.LogLine("Warning: Iterating took " + duration.Milliseconds + "ms, should be less than 1s.");
                }
                else _mainForm.LogLine("Iterating took " + duration.Milliseconds + "ms", true);
                Thread.Sleep(1000 - duration.Milliseconds);
            }

            ramFreeCounter.Close();
            pc.Close();
            myipTimer.Dispose();
            volume.Dispose();
        }

        private void StartBw(object state)
        {
            _getIpsBw.RunWorkerAsync(false);
        }

        private void SendInfo(String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            _conn.Send(bytes, bytes.Length);
        }

        public static Double CalculateCpu(CounterSample oldSample, CounterSample newSample, int rounding)
        {
            double difference = newSample.RawValue - oldSample.RawValue;
            double timeInterval = newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec;
            if (timeInterval == 0) return 0;
            var division = (difference/timeInterval)-1;
            if (division > 1) return 100;
            if (division < 0) return 0;
            return Math.Round(100 * (1 - (difference / timeInterval)), rounding);
        }

        private void GetIps(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            if (_options.MyipHost.Length == 0)
            {
                _mainForm.LogLine("MyIP host not set, skipping IP detection.");
                _remoteIp = "0.0.0.0";
                _localIp = "0.0.0.0";
                return;
            }
            _mainForm.LogLine("Trying to resolve internal and external IP...", true);
            try
            {
                _localIp = "0.0.0.0";
                var client = new TcpClient(_options.MyipHost, _options.MyipPort) { NoDelay = true };
                var s = client.GetStream();
                var stringToSend = "GET " + _options.MyipUrl + " HTTP/1.0\n\n";
                s.Write(Encoding.ASCII.GetBytes(stringToSend), 0, stringToSend.Length);
                _localIp = client.Client.LocalEndPoint.ToString().Split(':')[0];
                if ((bool)doWorkEventArgs.Argument)
                {
                    _options.NetIndex = GetNetworkAdapterIndex((IPEndPoint) client.Client.LocalEndPoint);
                    CreateNetworkCounters();
                }
                var streamReader = new StreamReader(s);
                var response = streamReader.ReadToEnd();
                streamReader.Close();
                client.Close();
                var match = Regex.Match(response, @"\b((1[0-9]{2}|2[0-4][0-9]|25[0-5]|[0-9]|[1-9][0-9])\.){3}(1[0-9]{2}|2[0-4][0-9]|25[0-5]|[0-9]|[1-9][0-9])", RegexOptions.RightToLeft);
                if (match.Success)
                {
                    _remoteIp = match.ToString();
                    _mainForm.LogLine("Found " + _localIp + " - " + _remoteIp, true);
                } else
                {
                    _mainForm.LogLine("Warning: HTTP return wasn't an IP.");
                }
                
            }
            catch (Exception e)
            {
                _mainForm.LogLine("Warning: Failed to retrieve external IP: " + e);
                _remoteIp = "0.0.0.0";
            }
        }

        private int GetNetworkAdapterIndex(IPEndPoint endPoint)
        {
            var netAdapter = GetNetAdapterNameByIp(endPoint);
            if (netAdapter == null)
            {
                _mainForm.LogLine("GetNetAdapterNameByIp(endPoint) = null", true);
                return 0;
            }
            var instanceNames = new PerformanceCounterCategory("Network Interface").GetInstanceNames();
            for (var i = 0; i < instanceNames.Length; i++)
            {
                if (!instanceNames[i].Equals(netAdapter)) continue;
                _mainForm.LogLine("Found Network Adapter: " + netAdapter, true);
                return i;
            }
            _mainForm.LogLine("Couldn't find Network Adapter: " + netAdapter, true);
            return 0;
        }

        private static String GetNetAdapterNameByIp(IPEndPoint endPoint)
        {
            var nets = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var n in nets)
            {
                if (n.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (var i in n.GetIPProperties().UnicastAddresses)
                    {
                        if (i.Address.AddressFamily != AddressFamily.InterNetwork) continue;
                        if (endPoint.Address.Equals(i.Address)) return n.Description.Replace('(', '[').Replace(')', ']').Replace('#', '_').Replace('/', '_').Replace('\\', '_');
                    }
                }
            }
            return null;
        }

        public String GetNp()
        {
            if (!_options.SendNp) return "";
            try
            {
                var file = new FileStream(_options.NpFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (file.Length < 4) return "";
                file.Seek(3, SeekOrigin.Begin);
                var buffer = new byte[file.Length - 3];
                file.Read(buffer, 0, (int)file.Length - 3);
                file.Close();
                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception e)
            {
                _mainForm.LogLine("Warning: Reading Now Playing -info failed: " + e, true);
                // _options.SendNp = false;
                // _mainForm.LogLine("Sending Now Playing -info disabled.");
                return "";
            }
        }
    }
}
