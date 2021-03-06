//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Settlement.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.tblStudentRooms = new HashSet<StudentRoom>();
        }
    
        public int Id { get; set; }
        public int HostelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Number { get; set; }
        public int AmountPlaces { get; set; }
        public int RoomFloor { get; set; }
    
        public virtual Hostel tblHostel { get; set; }
        public virtual RoomType tblRoomType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRoom> tblStudentRooms { get; set; }
    }
}
