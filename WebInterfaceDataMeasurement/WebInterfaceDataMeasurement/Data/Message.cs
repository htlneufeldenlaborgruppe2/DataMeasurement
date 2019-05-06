using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Message
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Dust { get; set; }
        public string Ldr { get; set; }
        public string Humidity { get; set; }
        public string Co2 { get; set; }
        public string Temp { get; set; }
        public DateTime? Timesent { get; set; }
        public string Noise { get; set; }
    }
}