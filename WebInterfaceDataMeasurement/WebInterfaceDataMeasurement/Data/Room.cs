using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Room
    {
        public Room()
        {
            RaumDevices = new HashSet<RaumDevice>();
        }

        public int RoomId { get; set; }
        public int RaumNr { get; set; }
        public decimal? NoiseConstant { get; set; }
        public int WorkingPlaces { get; set; }

        public virtual ICollection<RaumDevice> RaumDevices { get; set; }
    }
}