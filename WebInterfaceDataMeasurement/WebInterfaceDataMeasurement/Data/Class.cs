using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Class
    {
        public Class()
        {
            RoomSubjects = new HashSet<RoomSubject>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int? Students { get; set; }

        public virtual ICollection<RoomSubject> RoomSubjects { get; set; }
    }
}