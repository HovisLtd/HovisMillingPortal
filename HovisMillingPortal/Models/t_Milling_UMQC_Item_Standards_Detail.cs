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
    
    public partial class t_Milling_UMQC_Item_Standards_Detail
    {
        public int StdDetailRecid { get; set; }
        public string StdRecipe { get; set; }
        public string StdItem { get; set; }
        public Nullable<int> MasterStdRecid { get; set; }
        public Nullable<double> StdMinScore { get; set; }
        public Nullable<double> StdTargetScore { get; set; }
        public Nullable<double> StdMaxScore { get; set; }
        public string StdComments { get; set; }
        public Nullable<int> StdActiveFlag { get; set; }
        public Nullable<long> RecipeProductRecid { get; set; }
    }
}
