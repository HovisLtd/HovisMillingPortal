using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;
using DevExpress.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace HovisMillingPortal.Controllers
{
    public class BacsDetailsController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();

        // GET: BacsCustomer
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult GridViewPartial()
        {
            var DetailSummary = (from a in db.t_Milling_Bacs_Detail
                                 group a by a.HeaderRecid into g
                                 select new
                                 {
                                     HeaderRecid = g.FirstOrDefault().HeaderRecid,
                                     TotalValue = g.Sum(x => x.ClaimedAmount).ToString()
                                 });

            //var model = db.t_Milling_Bacs_Header.OrderByDescending(s => s.HeaderRecid).ToList();

            var model = (from a in db.t_Milling_Bacs_Header
                         join b in DetailSummary on a.HeaderRecid equals b.HeaderRecid
                         select new
                         {
                                 HeaderRecid = a.HeaderRecid,
                                 PaymentDate = a.PaymentDate,
	                             StageFlag = a.StageFlag,
                                 DDFrequency = a.DDFrequency,
                                 BacsID = a.BacsID,
	                             MasterBankSortCode = a.MasterBankSortCode,
                                 MasterBankAccountNo = a.MasterBankAccountNo,
                                 MasterBankAccountName = a.MasterBankAccountName,
                                 CreatedBy = a.CreatedBy,
                                 LastChangedBy = a.LastChangedBy,
                                 CreatedDate = a.CreatedDate,
                                 LastChangedDate = a.LastChangedDate,
                                 SentBy = a.SentBy,
                                 SentDate = a.SentDate,
                                 AuthorisedBy = a.AuthorisedBy,
                                 AuthorisedDate = a.AuthorisedDate,
                                 RejectedFlag = a.RejectedFlag,
                                 TotalValue = b.TotalValue
                          }).OrderByDescending(x => x.HeaderRecid).ToList();

            ViewBag.StageFlag = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");

            return PartialView("_GridViewPartial", model);
        }

        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult Create()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            t_Milling_Bacs_Header t_Milling_Bacs_Header = new t_Milling_Bacs_Header();

            var Masterdata = db.t_Milling_Bacs_Master_Data.FirstOrDefault();

            t_Milling_Bacs_Header.StageFlag = 1;
            t_Milling_Bacs_Header.PaymentDate = adjusteddate;
            t_Milling_Bacs_Header.BacsID = Masterdata.BacsID;
            t_Milling_Bacs_Header.MasterBankSortCode = Masterdata.MasterBankSortCode;
            t_Milling_Bacs_Header.MasterBankAccountNo = Masterdata.MasterBankAccountNo;
            t_Milling_Bacs_Header.MasterBankAccountName = Masterdata.MasterBankAccountName;
            t_Milling_Bacs_Header.LastChangedBy = User.Identity.Name;
            t_Milling_Bacs_Header.LastChangedDate = adjusteddate;
            t_Milling_Bacs_Header.CreatedBy = User.Identity.Name;
            t_Milling_Bacs_Header.CreatedDate = adjusteddate;
            t_Milling_Bacs_Header.RejectedFlag = 0;

            ViewBag.StageFlag = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
 
            return View(t_Milling_Bacs_Header);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] HovisMillingPortal.Models.t_Milling_Bacs_Header item)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            if (item.PaymentDate == null)
            {
                ModelState.AddModelError("PaymentDate", "You must choose a payment date");
            }
            if (item.PaymentDate < adjusteddate)
            {
                ModelState.AddModelError("PaymentDate", "Payment date cannot be in the past");
            }
            if (item.DDFrequency == null)
            {
                ModelState.AddModelError("DDFrequency", "You must choose a Frequency");
            }

            var model = db.t_Milling_Bacs_Header;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Detail_Creation(item.HeaderRecid);
                    return RedirectToAction("IndexBacsDetails", "BacsDetails", new { headerrecid = item.HeaderRecid });
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            ViewBag.StageFlag = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");

            return View(item);
        }

        // GET: BacsCustomer
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult IndexBacsDetails(System.Int64 headerrecid)
        {
            ViewBag.headerrecid = headerrecid;
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult BacsDetailsGridViewPartial(System.Int64 headerrecid)
        {
            var model = db.t_Milling_Bacs_Detail.Where(s => s.HeaderRecid == headerrecid).ToList();

            ViewBag.StageFlag = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");

            return PartialView("_BacsDetailsGridViewPartial", model);
        }

        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<HovisMillingPortal.Models.t_Milling_Bacs_Detail, int> batchValues, System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    InsertNewItem(item, batchValues, headerrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    UpdateItem(item, batchValues, headerrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                DeleteItem(itemKey, batchValues, headerrecid);
            }

            var model = db.t_Milling_Bacs_Detail.Where(s => s.HeaderRecid == headerrecid).ToList();

            return PartialView("_BacsDetailsGridViewPartial", model);
        }

        public void InsertNewItem(HovisMillingPortal.Models.t_Milling_Bacs_Detail postedItem, MVCxGridViewBatchUpdateValues<HovisMillingPortal.Models.t_Milling_Bacs_Detail, int> batchValues, System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            // DDFrequency 7 is AUDDIS
            if (postedItem.DDFrequency == 7)
            {
                if (postedItem.ClaimedAmount != 0)
                {
                    batchValues.SetErrorText(postedItem, "Cannot have a value for AUDDIS....");
                    return;
                }
            }

            //We need to check if qty is zero
            if (postedItem.ClaimedAmount == null)
            {
                batchValues.SetErrorText(postedItem, "you must enter a quantity....");
                return;
            }

            if (postedItem.ClaimedAmount < 0)
            {
                batchValues.SetErrorText(postedItem, "cannot be less than zero....");
                return;
            }

            if (User.Identity.Name != null)
            {
                postedItem.LastChangedBy = User.Identity.Name;

            }
            postedItem.LastChangedDate = adjusteddate;
 
            var model = db.t_Milling_Bacs_Detail;
            try
            {
                model.Add(postedItem);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                batchValues.SetErrorText(postedItem, e.Message);
            }
        }

        public void UpdateItem(HovisMillingPortal.Models.t_Milling_Bacs_Detail postedItem, MVCxGridViewBatchUpdateValues<HovisMillingPortal.Models.t_Milling_Bacs_Detail, int> batchValues, System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            var headermodel = db.t_Milling_Bacs_Header.FirstOrDefault(e => e.HeaderRecid == headerrecid);
            var item = db.t_Milling_Bacs_Detail.First(i => i.DetailRecid == postedItem.DetailRecid);

            // DDFrequency 7 is AUDDIS
            if (item.DDFrequency == 7)
            {
                if (postedItem.ClaimedAmount != 0)
                {
                    batchValues.SetErrorText(postedItem, "Cannot have a value for AUDDIS....");
                    return;
                }
            }

            //We need to check if qty is zero
            if (postedItem.ClaimedAmount == null)
            {
                batchValues.SetErrorText(postedItem, "you must enter an amount or zero....");
                return;
            }

            if (postedItem.ClaimedAmount < 0)
            {
                batchValues.SetErrorText(postedItem, "cannot be less than zero....");
                return;
            }

            if (User.Identity.Name != null)
            {
                postedItem.LastChangedBy = User.Identity.Name;
            }
            postedItem.LastChangedDate = adjusteddate;

            var modelItem = db.t_Milling_Bacs_Detail.Find(postedItem.DetailRecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.ClaimedAmount = postedItem.ClaimedAmount;
                    modelItem.LastChangedBy = postedItem.LastChangedBy;
                    modelItem.LastChangedDate = postedItem.LastChangedDate;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                batchValues.SetErrorText(postedItem, e.Message);
            }
        }

        public void DeleteItem(int itemKey, MVCxGridViewBatchUpdateValues<HovisMillingPortal.Models.t_Milling_Bacs_Detail, int> batchValues, System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
        }

        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult IndexAuthorise(System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ViewBag.headerrecid = headerrecid;

            var modelItem = db.t_Milling_Bacs_Header.Find(headerrecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.StageFlag = 2;
                    modelItem.RejectedFlag = 0;
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Audit_Creation(headerrecid, User.Identity.Name, "Sent To Finance to Authorise", 1, 2, adjusteddate);
                    return RedirectToAction("Index", "BacsDetails");
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return View();
        }

        [Authorize(Roles = "Admin, BacsAuthorise")]
        public ActionResult IndexTreasuryAuthorise(System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ViewBag.headerrecid = headerrecid;

            var modelItem = db.t_Milling_Bacs_Header.Find(headerrecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.StageFlag = 3;
                    modelItem.AuthorisedBy = User.Identity.Name;
                    modelItem.AuthorisedDate = adjusteddate;
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Audit_Creation(headerrecid, User.Identity.Name, "Authorised by Treasury", 7, 3, adjusteddate);
                    return RedirectToAction("Index", "BacsDetails");
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return View();
        }

        [Authorize(Roles = "Admin, BacsAuthorise")]
        public ActionResult IndexTreasuryReject(System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ViewBag.headerrecid = headerrecid;

            var modelItem = db.t_Milling_Bacs_Header.Find(headerrecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.StageFlag = 1;
                    modelItem.AuthorisedBy = "Rejected By - " + User.Identity.Name;
                    modelItem.AuthorisedDate = adjusteddate;
                    modelItem.RejectedFlag = 1;
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Audit_Creation(headerrecid, User.Identity.Name, "Rejected by Treasury", 7, 1, adjusteddate);
                    return RedirectToAction("Index", "BacsDetails");
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return View();
        }

        // GET: BacsCustomer
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult IndexView(System.Int64 headerrecid)
        {
            ViewBag.headerrecid = headerrecid;
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult ViewBacsDetailsGridViewPartial(System.Int64 headerrecid)
        {
            var model = db.t_Milling_Bacs_Detail.Where(s => s.HeaderRecid == headerrecid).ToList();


            ViewBag.StageFlag = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");

            return PartialView("_ViewBacsDetailsGridViewPartial", model);
        }

        [Authorize(Roles = "Admin, BacsFinAuthorise")]
        public ActionResult IndexFinanceAuthorise(System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ViewBag.headerrecid = headerrecid;

            var modelItem = db.t_Milling_Bacs_Header.Find(headerrecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.StageFlag = 7;
                    modelItem.AuthorisedBy = User.Identity.Name;
                    modelItem.AuthorisedDate = adjusteddate;
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Audit_Creation(headerrecid, User.Identity.Name, "Authorised by Finance", 2, 7, adjusteddate);
                    return RedirectToAction("Index", "BacsDetails");
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return View();
        }

        [Authorize(Roles = "Admin, BacsFinAuthorise")]
        public ActionResult IndexFinanceReject(System.Int64 headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ViewBag.headerrecid = headerrecid;


            var modelItem = db.t_Milling_Bacs_Header.Find(headerrecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.StageFlag = 1;
                    modelItem.AuthorisedBy = "Rejected By - " + User.Identity.Name;
                    modelItem.AuthorisedDate = adjusteddate;
                    modelItem.RejectedFlag = 1;
                    db.SaveChanges();
                    db.usp_Milling_Bacs_Audit_Creation(headerrecid, User.Identity.Name, "Rejected by Finance", 2, 1, adjusteddate);
                    return RedirectToAction("Index", "BacsDetails");
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult IndexAudit()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            Session["AuditDateEdit"] = adjusteddate;
            //TempData["TrackerDateEdit"] = TrackerDateEdit;
            ViewBag.AuditDateEdit = adjusteddate;
            if (Request.IsAjaxRequest()) //IsCallback
                return PartialView();
            return View(); //otherwise load the view that loads the partialview + rootlayout. Like reloading whole page.
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult IndexAudit(DateTime? AuditDateEdit)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            if (AuditDateEdit == null && TempData["AuditDateEdit"] != null)
            {
                AuditDateEdit = (DateTime?)TempData["AuditDateEdit"];
            }
            if (AuditDateEdit == null)
            {
                AuditDateEdit = adjusteddate;
            }

            Session["AuditDateEdit"] = AuditDateEdit;
            //TempData["TrackerDateEdit"] = TrackerDateEdit;
            ViewBag.AuditDateEdit = AuditDateEdit;
            if (Request.IsAjaxRequest()) //IsCallback
                return PartialView();
            return View(); //otherwise load the view that loads the partialview + rootlayout. Like reloading whole page.
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult AuditDetail_GridViewPartial(DateTime? AuditDateEdit)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            //ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Stage = new SelectList(db.t_Milling_Bacs_Stage, "StageRecid", "StageDesc");

            var model = (from a in db.t_Milling_Bacs_Audit_Details
                         where DbFunctions.TruncateTime(a.LastChangedDate) >= DbFunctions.TruncateTime(AuditDateEdit)
                         select new 
                         {
                             AuditRecid = a.AuditRecid,
                             BacsHeaderRecid = a.BacsHeaderRecid,
                             CurrentStage = a.CurrentStage,
                             NextStage = a.NextStage,
                             LastChangedBy = a.LastChangedBy,
                             LastChangedDate = a.LastChangedDate,
                             AuditText = a.AuditText
                         }).OrderByDescending(s => s.AuditRecid).ToList();

            TempData["AuditDateEdit"] = AuditDateEdit;
            ViewBag.AuditDateEdit = AuditDateEdit;

            return PartialView("_AuditDetail_GridViewPartial", model);
        }
    }
}