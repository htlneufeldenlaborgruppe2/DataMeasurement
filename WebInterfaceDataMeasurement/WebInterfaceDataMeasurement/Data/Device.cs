using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Device
    {
        public Device()
        {
            Messages = new HashSet<Message>();
            RaumDevices = new HashSet<RaumDevice>();
        }

        public int DeviceId { get; set; }
        public string IpAdress { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<RaumDevice> RaumDevices { get; set; }
    }
}