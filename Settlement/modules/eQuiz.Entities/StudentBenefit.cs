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
    
    public partial class StudentBenefit
    {
        public int Id { get; set; }
        public int BenefitId { get; set; }
        public int StudentId { get; set; }
    
        public virtual Benefit tblBenefit { get; set; }
        public virtual Student tblStudent { get; set; }
    }
}
