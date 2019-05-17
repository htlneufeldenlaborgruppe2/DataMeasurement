using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class RaumDevice
    {
        public int RaumDeviceId { get; set; }
        public int RaumId { get; set; }
        public int DeviceId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }

        public virtual Device Device { get; set; }
        public virtual Room Raum { get; set; }
    }
}