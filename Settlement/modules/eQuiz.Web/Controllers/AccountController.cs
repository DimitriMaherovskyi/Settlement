using Settlement.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Settlement.Web.Models;
using System.Web.Security;
using Settlement.Repositories.Abstract;
using eQuiz.Entities;
using Newtonsoft.Json;

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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [AllowAnonymous]
        public ActionResult Index(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _repository.Get<tblUsers>(u => u.UserName == model.Username && u.PasswordHash == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var role = _repository.Get<tblRoles>(r => r.RoleId == user.RoleId).FirstOrDefault();

                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.UserId;
                    serializeModel.FirstName = user.UserName;
                    serializeModel.LastName = user.Email;
                    serializeModel.userRole = role.RoleName;

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Email,
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

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.User = null;
            return RedirectToAction("Index", "Account", null);
        }
    }
}
    