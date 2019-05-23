using System;
using Microsoft.Azure.Devices.Client;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace RaspReceiver1 {
    class Program {
        const string DEVICEID = "Raspberry11";
        static SerialPort port;


        static DeviceClient _deviceclient;
        static string _deviceConnectionString = "HostName=iothub01-htlneufelden.azure-devices.net;DeviceId=device01;SharedAccessKey=nYVv4CjszOVifBH4OfQ6eP5AT5SyapOzlFrnO9bRoq8=";
        static System.Globalization.NumberFormatInfo formatInfo;

        static void Main(string[] args) {
            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = ".";
            formatInfo = numberFormat;
            Console.WriteLine("Hello World!");
            port = new SerialPort("/dev/ttyUSB0", 9600);
            port.DataReceived += (o, e) => Task.Run(() => Port_DataReceived(o, e));
            port.Open();
            do {
                port.Write(new byte[] { 1 }, 0, 1);
                System.Threading.Thread.Sleep(20000);

            } while (true);

        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            string data = port.ReadLine();



            Dictionary<string, object> returnDict = new Dictionary<string, object>();
            returnDict.Add("deviceID", DEVICEID);
            returnDict.Add("timesent", DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.ff"));

            foreach (var item in data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
                string[] splitBursch = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitBursch[0] == "noisevalues") {
                    string[] valuesSplit = splitBursch[1].Split('_');
                    double[] valuesDouble = new double[valuesSplit.Length];

                    for (int i = 0; i < valuesSplit.Length; i++) {
                        if (valuesSplit[i].Double.TryParse(splitBursch[1], System.Globalization.NumberStyles.Any, formatInfo, out double res)) {
                            valuesDouble[i] = valuesSplit[i];
                        }
                    }
                    double median = CalculateMedian(valuesDouble);
                    double lowerQuartil = CalculateLowerQuartil(valuesDouble, median);
                    double upperQuartil = CalculateUpperQuartil(valuesDouble, median);

                } else if (Double.TryParse(splitBursch[1], System.Globalization.NumberStyles.Any, formatInfo, out double res)) {
                    returnDict.Add(splitBursch[0], res);
                } else {
                    Console.WriteLine("Failed parsing to double: " + splitBursch[1]);
                }


            }





        }
        //private static void SendToApi(string json)
        //{
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "POST";

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {

        //        streamWriter.Write(json);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }

        //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //}

        private static void SendToIotHub(string json) {
            _deviceclient = DeviceClient.CreateFromConnectionString(_deviceConnectionString, TransportType.Mqtt);
            Console.WriteLine(json);
            Message message = new Message(Encoding.ASCII.GetBytes(json));
            _deviceclient.SendEventAsync(message);
        }

        private static double CalculateMedian(double[] noiseValues) {

            noiseValues.Sort();
            int length = noiseValues.Length;

            for (int i = 0; i < length; i++) {
                if (length % 2 == 0) {
                    return noiseValues[length / 2];
                } else {
                    int lengthOben = length / 2 + 0.5;
                    int lengthUnten = length / 2 - 0.5;

                    return (noiseValues[lengthOben] + noiseValues[lengthUnten]) / 2;
                }
            }
        }

        private static double CalculateLowerQuartil(double[] noiseValues, double median) {

            noiseValues.Sort();
            int length = noiseValues.Length;

            for (int i = 0; i < length / 2; i++) {
                double[] lowerHalf = noiseValues[i];
                return CalculateMedian(lowerHalf);
            }
        }

        private static double CalculateUpperQuartil(double[] noisValues, double median) {

            noiseValues.Sort();
            int length = noiseValues.Length;

            for (int i = length; i < length / 2; i--) {
                double upperHalf = noiseValues[i];
                return CalculateMedian(upperHalf);
            }
        }
    }
}
