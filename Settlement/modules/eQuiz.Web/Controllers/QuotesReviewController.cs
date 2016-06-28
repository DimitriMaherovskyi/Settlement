﻿using eQuiz.Entities;
using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Controllers
{
    public class QuotesReviewController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public QuotesReviewController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web actions

        [HttpGet]
        public JsonResult GetQuotes()
        {
            var result = new List<object>();

            var users = _repository.Get<tblUsers>();
            var roles = _repository.Get<tblRoles>();

            var query = from u in users
                        join r in roles on u.RoleId equals r.RoleId
                        select new
                        {
                            UserId = u.UserId,
                            Name = u.FirstName + " " + u.LastName,
                            Status = r.RoleName,
                            Institute = "IKNI",
                            Quote = u.Quote
                        };
            
            foreach (var item in query)
            {
                result.Add(item);
            } 

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void ChangeQuote(int userId, int newValue)
        {
            var user = _repository.GetSingle<tblUsers>(u => u.UserId == userId);
            user.Quote = newValue;

            _repository.Update<tblUsers>(user);
        }

        #endregion
    }
}