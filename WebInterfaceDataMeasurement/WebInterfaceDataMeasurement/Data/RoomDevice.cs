using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class RoomDevice
    {
        public int RoomDeviceId { get; set; }
        public int FkRoomId { get; set; }
        public int FkDeviceId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }

        public virtual Device FkDevice { get; set; }
        public virtual Room FkRoom { get; set; }
    }
}