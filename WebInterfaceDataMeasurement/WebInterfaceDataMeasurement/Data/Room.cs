using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Room
    {
        public Room()
        {
            RoomDevices = new HashSet<RoomDevice>();
            RoomSubjects = new HashSet<RoomSubject>();
        }

        public int RoomId { get; set; }
        public string RoomNr { get; set; }
        public decimal? NoiseConstant { get; set; }
        public int WorkingPlaces { get; set; }

        public virtual ICollection<RoomDevice> RoomDevices { get; set; }
        public virtual ICollection<RoomSubject> RoomSubjects { get; set; }
    }
}