//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HovisMillingPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_Milling_Bacs_Audit_Details
    {
        public long AuditRecid { get; set; }
        public Nullable<long> BacsHeaderRecid { get; set; }
        public Nullable<int> CurrentStage { get; set; }
        public Nullable<int> NextStage { get; set; }
        public string LastChangedBy { get; set; }
        public Nullable<System.DateTime> LastChangedDate { get; set; }
        public string AuditText { get; set; }
    }
}
