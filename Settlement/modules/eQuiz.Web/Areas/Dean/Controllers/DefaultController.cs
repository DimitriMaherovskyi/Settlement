using Settlement.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Areas.Dean.Controllers
{
    public class DefaultController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}