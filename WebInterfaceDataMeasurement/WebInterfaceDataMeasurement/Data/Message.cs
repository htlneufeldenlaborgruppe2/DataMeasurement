using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Message
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public double? Dust { get; set; }
        public double? Ldr { get; set; }
        public double? Humidity { get; set; }
        public double? Co2 { get; set; }
        public double? Temp { get; set; }
        public DateTime? Timesent { get; set; }
        public double? Noise { get; set; }
        public double? Noisemin { get; set; }
        public double? Noisemax { get; set; }
        public int? FkDeviceId { get; set; }
        public double? Noisequartal1 { get; set; }
        public double? Noisequartal3 { get; set; }

        public virtual Device FkDevice { get; set; }
    }
}