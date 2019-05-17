using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class Teacher
    {
        public Teacher()
        {
            RoomSubjectFkTeachers = new HashSet<RoomSubject>();
            RoomSubjectFkTeachers2 = new HashSet<RoomSubject>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public virtual ICollection<RoomSubject> RoomSubjectFkTeachers { get; set; }
        public virtual ICollection<RoomSubject> RoomSubjectFkTeachers2 { get; set; }
    }
}