using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;

namespace HovisMillingPortal.Controllers
{
    public class tickerController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();

        // GET: ticker
        public ActionResult Index()
        {
            return View();
        }

        // GET: SitePlantAuth
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult tickerIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult tickerGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_ticker.ToList();
            return PartialView("_tickerGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult tickerGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_ticker item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_ticker;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_ticker.ToList();
            return PartialView("_tickerGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult tickerGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_ticker item)
        {

            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            string updatedby = "";
            if (User.Identity.Name != null)
            {
                updatedby = User.Identity.Name;
            }


            var model = db.t_Milling_ticker;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.tickerRecid == item.tickerRecid);
                    if (modelItem != null)
                    {
                        modelItem.LastChangedBy = updatedby;
                        modelItem.LastChangedDate = adjusteddate;
                        modelItem.tickerData = item.tickerData;
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_ticker.ToList();
            return PartialView("_tickerGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult tickerGridViewPartialDelete(System.Int32 tickerRecid)
        {
            var model = db.t_Milling_ticker;
            if (tickerRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.tickerRecid == tickerRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var Newmodel = db.t_Milling_ticker.ToList();
            return PartialView("_tickerGridViewPartial", Newmodel);
        }
    }
}