using System;
using Microsoft.Azure.Devices.Client;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;

namespace RaspReceiver1
{
    class Program
    {
        const string DEVICEID = "Raspberry11";
        static SerialPort port;

        static DeviceClient _deviceclient;
        static string _deviceConnectionString = "HostName=iothub01-htlneufelden.azure-devices.net;DeviceId=device01;SharedAccessKey=nYVv4CjszOVifBH4OfQ6eP5AT5SyapOzlFrnO9bRoq8=";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            port = new SerialPort("/dev/ttyUSB0", 9600);
            port.DataReceived += Port_DataReceived;
            port.Open();
            do
            {
                port.Write(new byte[] { 1 }, 0, 1);
                Console.ReadKey();

            } while (true);

        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadLine();

            Console.Write(data);

            Dictionary<string, string> returnDict = new Dictionary<string, string>();
            returnDict.Add("deviceID", DEVICEID);

            foreach (var item in data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] splitBursch = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                returnDict.Add(splitBursch[0], splitBursch[1]);
            }


            _deviceclient = DeviceClient.CreateFromConnectionString(_deviceConnectionString, TransportType.Mqtt);
            
            Message message = new Message(Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(returnDict)));
            
            _deviceclient.SendEventAsync(message);
        }
    }
}
