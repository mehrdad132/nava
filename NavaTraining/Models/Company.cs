//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NavaTraining.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public Nullable<int> DurationWork { get; set; }
        public string DescPosition { get; set; }
    
        public virtual UserLogin UserLogin { get; set; }
    }
}
