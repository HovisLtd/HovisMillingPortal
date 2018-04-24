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
    public class BacsCustomerController : Controller
    {

        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();

        // GET: BacsCustomer
        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult GridViewPartial()
        {
            var BacsCustomers = (from A in db.t_Milling_Bacs_Customers
                            select new
                            {
                                CustomerRecid = A.CustomerRecid,
                                CustomerNo = A.CustomerNo,
                                SortingCode = A.SortingCode,
                                BankAccountNo = A.BankAccountNo,
                                BankAccountName = A.BankAccountName,
                                Status = A.Status,
                                BankMandateDate = A.BankMandateDate,
                                DDFrequency = A.DDFrequency,
                                NewBankMandate = A.NewBankMandate,
                                FirstClaim = A.FirstClaim,
                                SecondClaim = A.SecondClaim,
                                CreatedBy = A.CreatedBy,
                                LastChangedBy = A.LastChangedBy,
                                CreatedDate = A.CreatedDate,
                                LastChangedDate = A.LastChangedDate
                            });

            var model = BacsCustomers.ToList();

            ViewBag.Status = new SelectList(db.t_Milling_Bacs_Status, "StatusRecid", "StatusDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
            ViewBag.NewBankMandate = new SelectList(db.t_Milling_Bacs_NewBankMandate, "MandateRecid", "MandateDesc");

            return PartialView("_GridViewPartial", model);
        }

        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult Create()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            t_Milling_Bacs_Customers t_Milling_Bacs_Customers = new t_Milling_Bacs_Customers();
            t_Milling_Bacs_Customers.Status = 1;
            t_Milling_Bacs_Customers.LastChangedBy = User.Identity.Name;
            t_Milling_Bacs_Customers.LastChangedDate = adjusteddate;
            t_Milling_Bacs_Customers.CreatedBy = User.Identity.Name;
            t_Milling_Bacs_Customers.CreatedDate = adjusteddate;

            ViewBag.Status = new SelectList(db.t_Milling_Bacs_Status, "StatusRecid", "StatusDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
            ViewBag.NewBankMandate = new SelectList(db.t_Milling_Bacs_NewBankMandate, "MandateRecid", "MandateDesc");

            return View(t_Milling_Bacs_Customers);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] HovisMillingPortal.Models.t_Milling_Bacs_Customers item)
        {
            if (item.CustomerNo == null || item.CustomerNo == "")
            {
                ModelState.AddModelError("CustomerNo", "Customer No cannot be blank");
            }
            if (item.SortingCode == null || item.SortingCode == "")
            {
                ModelState.AddModelError("SortingCode", "Customers Bank Sort code cannot be blank");
            }
            if (item.BankAccountNo == null || item.BankAccountNo == "")
            {
                ModelState.AddModelError("BankAccountNo", "Customers Bank Account No cannot be blank");
            }
            if (item.BankAccountName == null || item.BankAccountName == "")
            {
                ModelState.AddModelError("BankAccountName", "Customers Bank Account Name cannot be blank");
            }
            if (item.DDFrequency == null)
            {
                ModelState.AddModelError("DDFrequency", "You must choose a Frequency");
            }
            if (item.NewBankMandate == null)
            {
                ModelState.AddModelError("NewBankMandate", "You must choose a Bank Mandate");
            }
            if (item.FirstClaim == null)
            {
                ModelState.AddModelError("FirstClaim", "You must enter a number");
            }
            if (item.FirstClaim == null)
            {
                ModelState.AddModelError("SecondClaim", "You must enter a number");
            }

            var model = db.t_Milling_Bacs_Customers;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            ViewBag.Status = new SelectList(db.t_Milling_Bacs_Status, "StatusRecid", "StatusDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
            ViewBag.NewBankMandate = new SelectList(db.t_Milling_Bacs_NewBankMandate, "MandateRecid", "MandateDesc");

            return View(item);
        }

        // GET: CustMain/Edit/5
        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult Edit(long? CustomerRecid)
        {
            if (CustomerRecid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Milling_Bacs_Customers t_Milling_Bacs_Customers = db.t_Milling_Bacs_Customers.Find(CustomerRecid);
            if (t_Milling_Bacs_Customers == null)
            {
                return HttpNotFound();
            }

            ViewBag.Status = new SelectList(db.t_Milling_Bacs_Status, "StatusRecid", "StatusDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
            ViewBag.NewBankMandate = new SelectList(db.t_Milling_Bacs_NewBankMandate, "MandateRecid", "MandateDesc");

            return View(t_Milling_Bacs_Customers);
        }

        // POST: CustMain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, BacsSupervisor, BacsClerk")]
        public ActionResult Edit([ModelBinder(typeof(DevExpressEditorsBinder))] HovisMillingPortal.Models.t_Milling_Bacs_Customers item)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            var model = db.t_Milling_Bacs_Customers;
            if (item.CustomerNo == null || item.CustomerNo == "")
            {
                ModelState.AddModelError("CustomerNo", "Customer No cannot be blank");
            }
            if (item.SortingCode == null || item.SortingCode == "")
            {
                ModelState.AddModelError("SortingCode", "Customers Bank Sort code cannot be blank");
            }
            if (item.BankAccountNo == null || item.BankAccountNo == "")
            {
                ModelState.AddModelError("BankAccountNo", "Customers Bank Account No cannot be blank");
            }
            if (item.BankAccountName == null || item.BankAccountName == "")
            {
                ModelState.AddModelError("BankAccountName", "Customers Bank Account Name cannot be blank");
            }
            if (item.DDFrequency == null)
            {
                ModelState.AddModelError("DDFrequency", "You must choose a Frequency");
            }
            if (item.NewBankMandate == null)
            {
                ModelState.AddModelError("NewBankMandate", "You must choose a Bank Mandate");
            }
            if (item.FirstClaim == null)
            {
                ModelState.AddModelError("FirstClaim", "You must enter a number");
            }
            if (item.FirstClaim == null)
            {
                ModelState.AddModelError("SecondClaim", "You must enter a number");
            }

            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    item.LastChangedBy = User.Identity.Name;
                    item.LastChangedDate = adjusteddate;
                }
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerRecid == item.CustomerRecid);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                        return RedirectToAction("Index", db.t_Milling_Bacs_Customers.ToList());
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewBag.Status = new SelectList(db.t_Milling_Bacs_Status, "StatusRecid", "StatusDesc");
            ViewBag.DDFrequency = new SelectList(db.t_Milling_Bacs_DDFrequency, "DDFreqRecid", "DDFreqDesc");
            ViewBag.NewBankMandate = new SelectList(db.t_Milling_Bacs_NewBankMandate, "MandateRecid", "MandateDesc");

            return View(item);
        }

    }
}