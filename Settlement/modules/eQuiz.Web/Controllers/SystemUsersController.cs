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
                        select new User(u.UserId, u.UserName, null, u.Email, r.RoleId, u.CreatedDate != null ? u.CreatedDate.ToString() : null, u.LastLoginDate != null ? u.LastLoginDate.ToString() : null, u.Quote, u.FirstName, u.LastName);

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
        public void AddUser(string Username, string Email, int RoleId, int Quote, string FirstName, string LastName, string Password)
        {
            var user = new tblUsers();

            string passwordHash = MD5CryptoProvider.ComputeHash(Password);
            user.UserName = Username;
            user.Email = Email;
            user.PasswordHash = passwordHash;
            user.RoleId = RoleId;
            user.CreatedDate = DateTime.Now;
            user.LastLoginDate = null;
            user.Quote = Quote;
            user.FirstName = FirstName;
            user.LastName = LastName;

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

        public void UpdateUserInfo(int UserId, string Username, string Email, int RoleId, int Quote, string FirstName, string LastName)
        {
            var user = _repository.GetSingle<tblUsers>(u => u.UserId == UserId);

            user.UserName = Username;
            user.RoleId = RoleId;
            user.Quote = Quote;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
        }

        public void DeleteUser(int id)
        {
            //_repository.Delete<tblUsers>("UserId", id);
        }
    }


    #endregion
}