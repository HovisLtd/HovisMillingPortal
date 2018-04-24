using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HovisMillingPortal.Models;
using DevExpress.Web.Mvc;

namespace HovisMillingPortal.Controllers
{
    public class ReportsController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexProdStandard()
        {
            return View();
        }

        [Authorize(Roles = "Admin, UMQCUser")]
        public ActionResult ProdStandardDocumentViewerPartial()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            ProductStandardMinMaxReport report = new ProductStandardMinMaxReport();

            DateTime STARTDATE = adjusteddate.AddDays(-7);
            report.Parameters["StartDate"].Value = STARTDATE;
            report.Parameters["EndDate"].Value = adjusteddate;

            report.Parameters["StandardTest"].Value = "0";
            report.Parameters["Recipe"].Value = "0";
            report.RequestParameters = false;

            return PartialView("_ProdStandardDocumentViewerPartial", report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }

        [Authorize(Roles = "Admin, UMQCUser")]
        public ActionResult ProdStandardDocumentViewerExport()
        {

            ProductStandardMinMaxReport report = new ProductStandardMinMaxReport();
            return DocumentViewerExtension.ExportTo(report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }

        public ActionResult IndexNoOfTests()
        {
            return View();
        }

        [Authorize(Roles = "Admin, UMQCUser")]
        public ActionResult NoOfTestsDocumentViewerPartial()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            TestNosGraphReport report = new TestNosGraphReport();

            DateTime STARTDATE = adjusteddate.AddDays(-7);
            report.Parameters["StartDate"].Value = STARTDATE;
            report.Parameters["EndDate"].Value = adjusteddate;

            //report.Parameters["StandardTest"].Value = "0";
            //report.Parameters["Recipe"].Value = "0";
            report.RequestParameters = false;

            return PartialView("_NoOfTestsDocumentViewerPartial", report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }

        [Authorize(Roles = "Admin, UMQCUser")]
        public ActionResult NoOfTestsDocumentViewerExport()
        {

            TestNosGraphReport report = new TestNosGraphReport();
            return DocumentViewerExtension.ExportTo(report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }

        public ActionResult BacsDetail(System.Int64? headerrecid)
        {
            ViewBag.headerrecid = headerrecid;
            return View();
        }

        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult BacsDetailDocumentViewerPartial(System.Int64? headerrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            BacsDetailReport report = new BacsDetailReport();

            ViewBag.headerrecid = headerrecid;
            report.Parameters["headerrecid"].Value = ViewBag.headerrecid;

            report.RequestParameters = false;

            return PartialView("_BacsDetailDocumentViewerPartial", report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }

        [Authorize(Roles = "Admin, BacsSupervisor, BacsAuthorise,BacsFinAuthorise, BacsClerk")]
        public ActionResult BacsDetailDocumentViewerExport(System.Int64? headerrecid)
        {
            ViewBag.headerrecid = headerrecid;
            BacsDetailReport report = new BacsDetailReport();
            return DocumentViewerExtension.ExportTo(report);
            //return PartialView("ProdBarcodeMissingIndex", model);
        }
    }
}