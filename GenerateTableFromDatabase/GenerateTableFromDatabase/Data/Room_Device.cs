//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerateTableFromDatabase.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room_Device
    {
        public int Room_Device_ID { get; set; }
        public int fk_RoomID { get; set; }
        public int fk_DeviceID { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidUntil { get; set; }
    
        public virtual Device Device { get; set; }
        public virtual Room Room { get; set; }
    }
}
