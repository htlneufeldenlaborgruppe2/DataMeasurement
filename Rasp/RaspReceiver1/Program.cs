    using System;
using Microsoft.Azure.Devices.Client;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaspReceiver1
{
    class Program
    {
        const string DEVICEID = "Raspberry11";
        static SerialPort port;

        static DeviceClient _deviceclient;
        static string _deviceConnectionString = "HostName=iothub01-htlneufelden.azure-devices.net;DeviceId=device01;SharedAccessKey=nYVv4CjszOVifBH4OfQ6eP5AT5SyapOzlFrnO9bRoq8=";
        static System.Globalization.NumberFormatInfo formatInfo;

        static void Main(string[] args)
        {
            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = ".";
            formatInfo = numberFormat;
            Console.WriteLine("Hello World!");
            port = new SerialPort("/dev/ttyUSB0", 9600);
            port.DataReceived += (o, e) => Task.Run(()=>Port_DataReceived(o,e));
            port.Open();
            do
            {
                port.Write(new byte[] { 1 }, 0, 1);
                System.Threading.Thread.Sleep(20000);

            } while (true);

        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadLine();

            

            Dictionary<string, object> returnDict = new Dictionary<string, object>();
            returnDict.Add("deviceID", DEVICEID);
            returnDict.Add("timesent", DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.ff"));

            foreach (var item in data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] splitBursch = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if(Double.TryParse(splitBursch[1],System.Globalization.NumberStyles.Any,formatInfo, out double res))
                {
                    returnDict.Add(splitBursch[0], res);
                }
                else
                {
                    Console.WriteLine("Failed parsing to double: " + splitBursch[1]);
                }

                
            }


            _deviceclient = DeviceClient.CreateFromConnectionString(_deviceConnectionString, TransportType.Mqtt);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(returnDict));
            Message message = new Message(Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(returnDict)));
            
            _deviceclient.SendEventAsync(message);
        }
    }
}
