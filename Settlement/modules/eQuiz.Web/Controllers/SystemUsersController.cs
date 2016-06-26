using eQuiz.Entities;
using Settlement.Repositories.Abstract;
using Settlement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Controllers
{
    public class SystemUsersController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public SystemUsersController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web actions

        // admin method
        [HttpGet]
        public JsonResult GetUsers()
        {
            var result = new List<object>();

            var users = _repository.Get<tblUsers>();
            var roles = _repository.Get<tblRoles>();

            var query = from u in users
                        join r in roles on u.RoleId equals r.RoleId
                        select new User(u.UserId, u.UserName, u.Password,  u.Email, r.RoleName);

            foreach (var item in query)
            {
                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // studcity worker method
        public JsonResult GetInstituteUsers()
        {
            var result = new List<object>();

            var users = _repository.Get<tblUsers>();
            var roles = _repository.Get<tblRoles>();

            var query = from u in users
                        join r in roles on u.RoleId equals r.RoleId
                        where r.RoleName != "Admin"
                        select new User(u.UserId, u.UserName, u.Password, u.Email, r.RoleName);

            foreach (var item in query)
            {
                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            var result = new List<object>();
            var roles = _repository.Get<tblRoles>();

            foreach (var item in roles)
            {
                result.Add(new Role(item.RoleId, item.RoleName));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void AddUser(string name, string email, string password, int roleId)
        {
            var user = new tblUsers();

            user.UserName = name;
            user.Email = email;
            user.Password = password;
            user.RoleId = roleId;
            user.CreatedDate = DateTime.Now;

            _repository.Insert<tblUsers>(user);
        }

        public void UpdateUserInfo(int id, string name, int roleId, int quote)
        {
            var user = _repository.GetSingle<tblUsers>(u => u.UserId == id);

            user.UserName = name;
            user.RoleId = roleId;
            //user.Quote = quote;
        }

        public void DeleteUser(int id)
        {

        }
    }


    #endregion
}