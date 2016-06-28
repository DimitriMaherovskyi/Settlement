using eQuiz.Entities;
using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settlement.Web.Controllers
{
    public class HostelsReviewController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public HostelsReviewController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web Actions

        [HttpGet]
        public JsonResult GetHostelsList()
        {
            var result = new List<object>();
            var hostels = _repository.Get<tblHostel>();

            foreach (var h in hostels)
            {
                result.Add(new { Id = h.Id, Number = h.Number, MonthPaymentSum = h.MonthPaymentSum });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void AddHostel(int number, string address, int monthPaymentSum)
        {
            var hostel = new tblHostel();
            hostel.Number = number;
            hostel.Address = address;
            hostel.MonthPaymentSum = monthPaymentSum;
        }

        [HttpPost]
        public void UpdateHostel(int hostelId, int number, string address, int monthPaymentSum)
        {
            var hostel = _repository.GetSingle<tblHostel>(h => h.Id == hostelId);

            hostel.Number = number;
            hostel.Address = address;
            hostel.MonthPaymentSum = monthPaymentSum;

            _repository.Update<tblHostel>(hostel);
        }

        #endregion
    }
}