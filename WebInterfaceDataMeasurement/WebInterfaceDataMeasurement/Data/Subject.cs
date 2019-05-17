using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Subject
    {
        public Subject()
        {
            RoomSubjects = new HashSet<RoomSubject>();
        }

        public int SubjectId { get; set; }
        public string SubjectNamelong { get; set; }
        public string SubjectNameshort { get; set; }

        public virtual ICollection<RoomSubject> RoomSubjects { get; set; }
    }
}