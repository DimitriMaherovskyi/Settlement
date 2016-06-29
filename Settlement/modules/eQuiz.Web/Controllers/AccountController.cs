using Settlement.Web.Code;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Settlement.Web.Models;
using System.Web.Security;
using Settlement.Repositories.Abstract;
using eQuiz.Entities;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Generic;
using Settlement.Web.Code;

namespace Settlement.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        public AccountController(IRepository repository)
        {
            this._repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                string passwordHash = MD5CryptoProvider.ComputeHash(model.Password);

                var user = _repository.Get<tblUsers>(u => u.UserName == model.Username && u.PasswordHash == passwordHash).FirstOrDefault();
                if (user != null)
                {
                    var role = _repository.Get<tblRoles>(r => r.RoleId == user.RoleId).FirstOrDefault();
                    user.LastLoginDate = DateTime.Now;
                    _repository.Update<tblUsers>(user);
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.UserId;
                    serializeModel.FirstName = user.FirstName;
                    serializeModel.LastName = user.LastName;
                    serializeModel.userRole = role.RoleName;

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.FirstName + " " + user.LastName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false, //pass here true, if you want to implement remember me functionality
                    userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    if (role.RoleName == "Admin")
                    {
                        return RedirectToAction("Index", "Default", new { area = "Admin" });
                    }
                    else if (role.RoleName.Contains("Warden"))
                    {
                        return RedirectToAction("Index", "Default", new { area = "Warden" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Default", new { area = "Dean" });
                    }
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(model);
        }

        [AuthorizeAccess(Roles = "Admin, Dean, Rector, Warden")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.User = null;
            return RedirectToAction("Index", "Account", null);
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            var result = new List<object>();
            var roles = _repository.Get<tblRoles>().ToList();

            foreach(var item in roles)
            {
                result.Add(new { RoleId = item.RoleId, RoleName = item.RoleName });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
    