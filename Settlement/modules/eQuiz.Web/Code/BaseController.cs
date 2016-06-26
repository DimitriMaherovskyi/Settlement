using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Settlement.Web.Models;

namespace Settlement.Web.Code
{
    public abstract class BaseController: Controller
    {
        protected virtual new UserPrincipal User
        {
            get { return HttpContext.User as UserPrincipal; }
        }
    }
}