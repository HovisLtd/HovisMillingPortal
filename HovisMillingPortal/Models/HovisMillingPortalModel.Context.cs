﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HovisMillingPortalEntities : DbContext
    {
        public HovisMillingPortalEntities()
            : base("name=HovisMillingPortalEntities")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<t_Milling_Bacs_Customers> t_Milling_Bacs_Customers { get; set; }
        public virtual DbSet<t_Milling_Bacs_DDFrequency> t_Milling_Bacs_DDFrequency { get; set; }
        public virtual DbSet<t_Milling_Bacs_Master_Data> t_Milling_Bacs_Master_Data { get; set; }
        public virtual DbSet<t_Milling_Bacs_NewBankMandate> t_Milling_Bacs_NewBankMandate { get; set; }
        public virtual DbSet<t_Milling_Bacs_Status> t_Milling_Bacs_Status { get; set; }
        public virtual DbSet<t_Milling_Bacs_Detail> t_Milling_Bacs_Detail { get; set; }
        public virtual DbSet<t_Milling_Bacs_Header> t_Milling_Bacs_Header { get; set; }
        public virtual DbSet<t_Milling_Bacs_Stage> t_Milling_Bacs_Stage { get; set; }
        public virtual DbSet<t_Milling_UMQC_Category> t_Milling_UMQC_Category { get; set; }
        public virtual DbSet<t_Milling_UMQC_Item_Standards_Detail> t_Milling_UMQC_Item_Standards_Detail { get; set; }
        public virtual DbSet<t_Milling_UMQC_Results_Standards_Master> t_Milling_UMQC_Results_Standards_Master { get; set; }
        public virtual DbSet<t_Milling_UMQC_Products_Master> t_Milling_UMQC_Products_Master { get; set; }
        public virtual DbSet<t_Milling_UMQC_Results_Detail> t_Milling_UMQC_Results_Detail { get; set; }
        public virtual DbSet<t_Milling_UMQC_Results_Header> t_Milling_UMQC_Results_Header { get; set; }
        public virtual DbSet<t_Milling_Function> t_Milling_Function { get; set; }
        public virtual DbSet<t_Milling_Plant> t_Milling_Plant { get; set; }
        public virtual DbSet<t_Milling_Shift> t_Milling_Shift { get; set; }
        public virtual DbSet<t_Milling_Site> t_Milling_Site { get; set; }
        public virtual DbSet<t_Hovis_Milling_User_Plant_Authorisation> t_Hovis_Milling_User_Plant_Authorisation { get; set; }
        public virtual DbSet<t_Hovis_Milling_User_Site_Authorisation> t_Hovis_Milling_User_Site_Authorisation { get; set; }
        public virtual DbSet<t_Milling_Bacs_Audit_Details> t_Milling_Bacs_Audit_Details { get; set; }
        public virtual DbSet<t_Milling_UMQC_Results_Standards_Master_ByFunction> t_Milling_UMQC_Results_Standards_Master_ByFunction { get; set; }
        public virtual DbSet<t_Milling_ticker> t_Milling_ticker { get; set; }
        public virtual DbSet<t_Hovis_Milling_User_Default_Site> t_Hovis_Milling_User_Default_Site { get; set; }
    
        public virtual int usp_Milling_Bacs_Detail_Creation(Nullable<long> headerrecid)
        {
            var headerrecidParameter = headerrecid.HasValue ?
                new ObjectParameter("headerrecid", headerrecid) :
                new ObjectParameter("headerrecid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Milling_Bacs_Detail_Creation", headerrecidParameter);
        }
    
        public virtual int usp_Milling_UMQC_Results_Detail_Creation(Nullable<long> resultheaderrecid)
        {
            var resultheaderrecidParameter = resultheaderrecid.HasValue ?
                new ObjectParameter("resultheaderrecid", resultheaderrecid) :
                new ObjectParameter("resultheaderrecid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Milling_UMQC_Results_Detail_Creation", resultheaderrecidParameter);
        }
    
        public virtual ObjectResult<usp_Milling_UMQC_Results_By_Recipe_and_Standard_Result> usp_Milling_UMQC_Results_By_Recipe_and_Standard(Nullable<long> resultmasterstdrRecid, Nullable<long> resultproductrecid, Nullable<System.DateTime> startdate, Nullable<System.DateTime> enddate)
        {
            var resultmasterstdrRecidParameter = resultmasterstdrRecid.HasValue ?
                new ObjectParameter("resultmasterstdrRecid", resultmasterstdrRecid) :
                new ObjectParameter("resultmasterstdrRecid", typeof(long));
    
            var resultproductrecidParameter = resultproductrecid.HasValue ?
                new ObjectParameter("resultproductrecid", resultproductrecid) :
                new ObjectParameter("resultproductrecid", typeof(long));
    
            var startdateParameter = startdate.HasValue ?
                new ObjectParameter("startdate", startdate) :
                new ObjectParameter("startdate", typeof(System.DateTime));
    
            var enddateParameter = enddate.HasValue ?
                new ObjectParameter("enddate", enddate) :
                new ObjectParameter("enddate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Milling_UMQC_Results_By_Recipe_and_Standard_Result>("usp_Milling_UMQC_Results_By_Recipe_and_Standard", resultmasterstdrRecidParameter, resultproductrecidParameter, startdateParameter, enddateParameter);
        }
    
        public virtual ObjectResult<usp_Milling_UMQC_Results_Graph_by_Product_Day_Result> usp_Milling_UMQC_Results_Graph_by_Product_Day(Nullable<System.DateTime> startdate, Nullable<System.DateTime> enddate)
        {
            var startdateParameter = startdate.HasValue ?
                new ObjectParameter("startdate", startdate) :
                new ObjectParameter("startdate", typeof(System.DateTime));
    
            var enddateParameter = enddate.HasValue ?
                new ObjectParameter("enddate", enddate) :
                new ObjectParameter("enddate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Milling_UMQC_Results_Graph_by_Product_Day_Result>("usp_Milling_UMQC_Results_Graph_by_Product_Day", startdateParameter, enddateParameter);
        }
    
        public virtual int usp_Milling_Bacs_Audit_Creation(Nullable<long> headerrecid, string username, string audittext, Nullable<int> currentstage, Nullable<int> nextstage, Nullable<System.DateTime> currentdatetime)
        {
            var headerrecidParameter = headerrecid.HasValue ?
                new ObjectParameter("headerrecid", headerrecid) :
                new ObjectParameter("headerrecid", typeof(long));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var audittextParameter = audittext != null ?
                new ObjectParameter("audittext", audittext) :
                new ObjectParameter("audittext", typeof(string));
    
            var currentstageParameter = currentstage.HasValue ?
                new ObjectParameter("currentstage", currentstage) :
                new ObjectParameter("currentstage", typeof(int));
    
            var nextstageParameter = nextstage.HasValue ?
                new ObjectParameter("nextstage", nextstage) :
                new ObjectParameter("nextstage", typeof(int));
    
            var currentdatetimeParameter = currentdatetime.HasValue ?
                new ObjectParameter("currentdatetime", currentdatetime) :
                new ObjectParameter("currentdatetime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Milling_Bacs_Audit_Creation", headerrecidParameter, usernameParameter, audittextParameter, currentstageParameter, nextstageParameter, currentdatetimeParameter);
        }
    
        public virtual ObjectResult<usp_Milling_Bacs_Indivdual_Detail_Report_Result> usp_Milling_Bacs_Indivdual_Detail_Report(Nullable<long> headerrecid)
        {
            var headerrecidParameter = headerrecid.HasValue ?
                new ObjectParameter("headerrecid", headerrecid) :
                new ObjectParameter("headerrecid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Milling_Bacs_Indivdual_Detail_Report_Result>("usp_Milling_Bacs_Indivdual_Detail_Report", headerrecidParameter);
        }
    }
}
