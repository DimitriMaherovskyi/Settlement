using Settlement.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string role)
        {
            switch(role)
            {
                case "dean":
                    return RedirectToAction("Index", "Default", new { area = "Dean" });
                case "warden":
                    return RedirectToAction("Index", "Default", new { area = "Warden" });                    
                case "admin":
                    return RedirectToAction("Index", "Default", new { area = "Admin" });                    
                default:
                    return View();
            }
            
        }
    }
}