using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;

namespace HovisMillingPortal.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();
        public ActionResult Index()
        {
            var tickerdata = db.t_Milling_ticker.FirstOrDefault();
            ViewBag.tickerData = tickerdata.tickerData;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}