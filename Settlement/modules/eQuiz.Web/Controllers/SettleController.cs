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
    public class SettleController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public SettleController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web actions

        [HttpGet]
        public JsonResult GetStudentsToSettle()
        {
            var result = new List<ShowStudent>();

            var students = _repository.Get<tblStudent>();
            var studentRooms = _repository.Get<tblStudentRoom>();
            var settleRequests = _repository.Get<tblSettleRequest>(sr => sr.Status == true);
            var studentResidences = _repository.Get<tblStudentResidence>();
            var residences = _repository.Get<tblResidence>();

            var studentBenefits = _repository.Get<tblStudentBenefit>();
            var benefits = _repository.Get<tblBenefit>();

            var noBenefitStudents = from s in students
                                    join sr in studentRooms on s.Id equals sr.StudentId
                                    join srq in settleRequests on s.Id equals srq.StudentId
                                    join sres in studentResidences on s.Id equals sres.StudentId
                                    join res in residences on sres.ResidenceId equals res.Id
                                    where srq.Status = true
                                    select new SettleStudent(s.Id, s.Firstname + " " + s.Surname, s.Insitute, s.GenderType, res.Name, res.Distance, 0, srq.Id);

            var benefitStudents = from s in students
                                  join sr in studentRooms on s.Id equals sr.StudentId
                                  join srq in settleRequests on s.Id equals srq.StudentId
                                  join sres in studentResidences on s.Id equals sres.StudentId
                                  join res in residences on sres.ResidenceId equals res.Id
                                  join sb in studentBenefits on s.Id equals sb.StudentId
                                  join b in benefits on sb.BenefitId equals b.Id
                                  where srq.Status = true
                                  select new SettleStudent(s.Id, s.Firstname + " " + s.Surname, s.Insitute, s.GenderType, res.Name, res.Distance, b.Value, srq.Id);

            foreach (var item in noBenefitStudents)
            {
                AutoSettle.Students.Add(item);
            }

            foreach (var item in benefitStudents)
            {
                AutoSettle.Students.Add(item);
            }

            AutoSettle.SetRatings();
            AutoSettle.OrderStudentsByRating();

            foreach (var item in AutoSettle.Students)
            {
                result.Add(new ShowStudent(item.Id, item.Name, item.Rating));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSettleResult()
        {
            var result = new List<ShowStudent>();

            var rooms = _repository.Get<tblRoom>();
            var hostels = _repository.Get<tblHostel>();

            var unsetteled = from aus in AutoSettle.Students
                             where aus.RoomId == null
                             select new ShowStudent(aus.Id, aus.Name, aus.Rating);

            var setteled = from aus in AutoSettle.Students
                           join r in rooms on aus.RoomId equals r.Id
                           join h in hostels on r.HostelId equals h.Id
                           select new ShowStudent(aus.Id, aus.Name, aus.Rating, h.Number, r.Number);

            foreach (var item in setteled)
            {
                result.Add(item);
            }

            foreach (var item in unsetteled)
            {
                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void SettleStudents()
        {
            AutoSettle.SettleStudents();
        }

        [HttpPost]
        public void DiscardChanges()
        {
            AutoSettle.Students = new List<SettleStudent>();
            AutoSettle.FreeRooms = new List<SettleRoom>();
        }

        [HttpPost]
        public void SaveChanges()
        {

        }

        #endregion

    }
}