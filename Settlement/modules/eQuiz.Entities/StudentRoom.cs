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
    
    public partial class StudentRoom
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public System.DateTime DateIn { get; set; }
        public System.DateTime DateOut { get; set; }
    
        public virtual Room tblRoom { get; set; }
        public virtual Student tblStudent { get; set; }
    }
}
