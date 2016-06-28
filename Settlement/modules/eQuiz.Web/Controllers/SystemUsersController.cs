using eQuiz.Entities;
using Settlement.Repositories.Abstract;
using Settlement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using Settlement.Web.Code;

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
                        select new User(u.UserId, u.UserName, null,  u.Email, r.RoleId, u.CreatedDate, u.LastLoginDate, u.Quote, u.FirstName, u.LastName);

            foreach (var item in query)
            {
                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // studcity worker method
        //public JsonResult GetInstituteUsers()
        //{
        //    var result = new List<object>();

        //    var users = _repository.Get<tblUsers>();
        //    var roles = _repository.Get<tblRoles>();

        //    var query = from u in users
        //                join r in roles on u.RoleId equals r.RoleId
        //                where r.RoleName != "Admin"
        //                select new User(u.UserId, u.UserName, u.PasswordHash, u.Email, r.RoleName);

        //    foreach (var item in query)
        //    {
        //        result.Add(item);
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

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
        public void AddUser(User userData)
        {
            var user = new tblUsers();

            string passwordHash = MD5CryptoProvider.ComputeHash(userData.Password);
            user.UserName = userData.Username;
            user.Email = userData.Email;
            user.PasswordHash = passwordHash;
            user.RoleId = userData.RoleId;
            user.CreatedDate = DateTime.Now;
            user.LastLoginDate = null;
            user.Quote = userData.Quote;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;

            var users = _repository.Get<tblUsers>();
            
            try
            {
                user.UserId = users.Count + 1;
                _repository.Insert<tblUsers>(user);
            }
            catch
            {
                for (var i = 1; i < users.Count + 1; i++)
                {
                    if (i != users[i].UserId)
                    {
                        user.UserId = i;
                        try
                        {
                            _repository.Insert<tblUsers>(user);
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
        }

        public void UpdateUserInfo(int id, string name, int roleId, int quote)
        {
            var user = _repository.GetSingle<tblUsers>(u => u.UserId == id);

            user.UserName = name;
            user.RoleId = roleId;
            user.Quote = quote;
        }

        public void DeleteUser(int id)
        {

        }
    }


    #endregion
}