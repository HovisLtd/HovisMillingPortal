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
    
    public partial class t_Milling_UMQC_Results_Header
    {
        public long ResultHeaderRecid { get; set; }
        public Nullable<int> ResultPlantRecid { get; set; }
        public Nullable<int> ResultFuncRecid { get; set; }
        public Nullable<System.DateTime> ResultCreationDate { get; set; }
        public Nullable<int> ResultShiftRecid { get; set; }
        public string ResultJobNo { get; set; }
        public Nullable<System.DateTime> ResultRunStart { get; set; }
        public Nullable<System.DateTime> ResultRunEnd { get; set; }
        public string ResultBinNumber { get; set; }
        public Nullable<long> ResultProductRecid { get; set; }
        public string ResultCreatedBy { get; set; }
        public string ResultLastChangedBy { get; set; }
        public Nullable<System.DateTime> ResultLastChangedDate { get; set; }
        public Nullable<int> ResultActiveFlag { get; set; }
        public Nullable<int> ExcludeAverages { get; set; }
        public string ConcessionDesc { get; set; }
        public Nullable<System.DateTime> ResultTestTaken { get; set; }
        public string WheatfeedBinNumber { get; set; }
        public string GristVersionNumber { get; set; }
        public Nullable<double> plannedJobtonnage { get; set; }
        public Nullable<double> finalJobtonnage { get; set; }
    }
}
