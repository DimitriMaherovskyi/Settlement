using Settlement.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Account");
        }

        public ActionResult MainLogin()
        {
            return View();
        }
    }
}