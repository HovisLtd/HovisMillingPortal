using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HovisMillingPortal.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Route("error/not-found")]
        [AllowAnonymous]
        public ActionResult Error404()
        {
            return View();
        }

        // GET: Error
        [Route("error/not-authorised")]
        [AllowAnonymous]
        public ActionResult Error403()
        {
            return View();
        }
    }
}