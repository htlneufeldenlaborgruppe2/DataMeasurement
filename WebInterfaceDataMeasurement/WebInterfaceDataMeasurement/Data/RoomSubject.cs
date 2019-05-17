using System;
using System.Collections.Generic;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class RoomSubject
    {
        public int FkRoomId { get; set; }
        public int FkTeacherId { get; set; }
        public int FkSubjectId { get; set; }
        public int FkClassId { get; set; }
        public int RoomSubjectId { get; set; }
        public bool? Test { get; set; }
        public bool? ReplacementLesson { get; set; }
        public int? Hour { get; set; }
        public DateTime? Day { get; set; }
        public int? FkTeachers2Id { get; set; }

        public virtual Class FkClass { get; set; }
        public virtual Room FkRoom { get; set; }
        public virtual Subject FkSubject { get; set; }
        public virtual Teacher FkTeacher { get; set; }
        public virtual Teacher FkTeachers2 { get; set; }
    }
}