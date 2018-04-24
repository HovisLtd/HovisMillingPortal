using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;

namespace HovisMillingPortal.Controllers
{
    public class BacsMasterDataController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();
        // GET: BacsMasterData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DDFrequencyIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult DDFrequencyGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Bacs_DDFrequency;
            return PartialView("_DDFrequencyGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult DDFrequencyGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_Bacs_DDFrequency item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Bacs_DDFrequency;
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
            return PartialView("_DDFrequencyGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult DDFrequencyGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_Bacs_DDFrequency item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Bacs_DDFrequency;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.DDFreqRecid == item.DDFreqRecid);
                    if (modelItem != null)
                    {
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
            return PartialView("_DDFrequencyGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult DDFrequencyGridViewPartialDelete(System.Int32 DDFreqRecid)
        {
            var model = db.t_Milling_Bacs_DDFrequency;
            if (DDFreqRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.DDFreqRecid == DDFreqRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_DDFrequencyGridViewPartial", model.ToList());
        }
    }
}