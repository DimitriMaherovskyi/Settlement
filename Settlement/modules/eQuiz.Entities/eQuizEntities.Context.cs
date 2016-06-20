﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eQuiz.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eQuizEntities : DbContext
    {
        public eQuizEntities()
            : base("name=eQuizEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<Hostel> Hostels { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Residence> Residences { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentBenefit> StudentBenefits { get; set; }
        public virtual DbSet<StudentResidence> StudentResidences { get; set; }
        public virtual DbSet<StudentRoom> StudentRooms { get; set; }
        public virtual DbSet<Violation> Violations { get; set; }
        public virtual DbSet<StudentViolation> StudentViolations { get; set; }
    }
}
