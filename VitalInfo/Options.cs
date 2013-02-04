using System;
using System.IO;
using System.Xml.Serialization;

namespace VitalInfo
{
    public class Options
    {
        public volatile bool PrintDebug = false;
        public volatile int NetIndex = 0;
        public volatile String PhoneIp = "192.168.0.8";
        public volatile int PhonePort = 25461;
        public volatile String MyipHost = "";
        public volatile int MyipPort = 80;
        public volatile String MyipUrl = "/myip.php";
        public volatile int MyipInterval = 60; // minutes
        public volatile bool SendNp = false;
        public volatile String NpFile = "";


        public static Options LoadFromFile(MainForm mainForm)
        {
            try
            {
                var file = new FileStream("VitalInfo.settings", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(typeof(Options));
                var options = (Options) serializer.Deserialize(file);
                file.Close();
                mainForm.LogLine("Loaded settings from file.");
                return options;
            }
            catch (Exception)
            {
                return new Options();
            }
        }

        public String SaveToFile()
        {
            try
            {
                var file = new FileStream("VitalInfo.settings", FileMode.Create, FileAccess.Write, FileShare.None);
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(file, this);
                file.Close();
                return null;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
