using System;

namespace WebInterfaceDataMeasurement.Controllers
{
    public class Message
    {
        public double primaryKey { get; set; }
        public string deviceid { get; set; }
        public DateTime datetime { get; set; }
        public double dust { get; set; }
        public double ldr { get; set; }
        public double co2 { get; set; }
        public double humidity { get; set; }
        public double temp { get; set; }
        public double noise { get; set; }
    }
}