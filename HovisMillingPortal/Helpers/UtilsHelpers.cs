using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;
using HovisMillingPortal.ViewModels;
using System.Collections.Generic;

namespace HovisMillingPortal.Helpers
{
    public class ProductHelpers
    {
       // HovisSOPV2.Models.HovisSOPEntities db = new HovisSOPV2.Models.HovisSOPEntities();

       // public List<productbyAPLVM> productbyAPL(long? APLNumber, DateTime? OrdDeliveryDate)
       // {
       // //var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
       // //var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
       // //var ProductCodesNonEDI = db.t_STG_SOP_Customer_Product_Data.Where(s => s.APLNumber == APLNumber).ToList().Select(s => new { productcode = s.Barcode, productDescription = string.Format("{0} - {1}", s.ItemNumber.Trim(), s.ItemDescription) });

       // var model = (from a in db.t_STG_SOP_Customer_Product_Data
       //              join b in db.t_STG_SOP_Cross_System_Product_Data on a.ItemNumber equals b.ItemNumber
       //              where DbFunctions.TruncateTime(OrdDeliveryDate) >= DbFunctions.TruncateTime(a.EffFromDate) && DbFunctions.TruncateTime(OrdDeliveryDate) <= DbFunctions.TruncateTime(a.EffToDate) && a.APLNumber == APLNumber
       //              select new productbyAPLVM
       //              {
       //                  CompanyCode = a.CompanyCode,
       //                  productcode = a.ItemNumber,
       //                  productDescription = a.ItemNumber.Trim() + a.ItemDescription.Trim() + (b.Unitsize != null ? (" - " + b.Unitsize) : "") + (b.IssueUnit != null ? (" - MOQ " + b.IssueUnit) : ""),
       //                  IssueUnit = b.IssueUnit,
       //                  Barcode = b.Barcode,
       //                  Unitsize = b.Unitsize,
       //                  EffFromDate = a.EffFromDate,
       //                  EffToDate = a.EffToDate,
       //              }).ToList();

       // return model;
       //}

    }
}