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
    [AuthorizeAccess(Roles = "Dean")]
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

            GetStudents();
            GetRooms();

            foreach (var item in AutoSettle.Students)
            {
                result.Add(new ShowStudent(item.Id, item.Name, item.Rating));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void GetStudents()
        {
            AutoSettle.Students = new List<SettleStudent>();
            var noBen = new List<SettleStudent>();
            var ben = new List<SettleStudent>();

            var students = _repository.Get<tblStudent>();
            var studentRooms = _repository.Get<tblStudentRoom>();
            var settleRequests = _repository.Get<tblSettleRequest>(sr => sr.Status == true);
            var studentResidences = _repository.Get<tblStudentResidence>();
            var residences = _repository.Get<tblResidence>();

            var studentBenefits = _repository.Get<tblStudentBenefit>();
            var benefits = _repository.Get<tblBenefit>();

            var noBenefitStudents = from s in students
                                        //join sr in studentRooms on s.Id equals sr.StudentId
                                    join srq in settleRequests on s.Id equals srq.StudentId
                                    join sres in studentResidences on s.Id equals sres.StudentId
                                    join res in residences on sres.ResidenceId equals res.Id
                                    where srq.Status = true
                                    select new SettleStudent(s.Id, s.Firstname + " " + s.Surname, s.Insitute, s.GenderType, res.Name, res.Distance, 0, srq.Id);

            var benefitStudents = from s in students
                                      //join sr in studentRooms on s.Id equals sr.StudentId
                                  join srq in settleRequests on s.Id equals srq.StudentId
                                  join sres in studentResidences on s.Id equals sres.StudentId
                                  join res in residences on sres.ResidenceId equals res.Id
                                  join sb in studentBenefits on s.Id equals sb.StudentId
                                  join b in benefits on sb.BenefitId equals b.Id
                                  where srq.Status = true
                                  select new SettleStudent(s.Id, s.Firstname + " " + s.Surname, s.Insitute, s.GenderType, res.Name, res.Distance, b.Value, srq.Id);

            foreach (var item in noBenefitStudents)
            {
                noBen.Add(item);
            }

            foreach (var item in benefitStudents)
            {
                ben.Add(item);
            }

            for (var i = 0; i < ben.Count; i++)
            {
                for (var j = 0; j < noBen.Count; j++)
                {
                    if (ben[i].Equals(noBen[j]))
                    {
                        noBen.Remove(noBen[j]);
                        break;
                    }
                }
            }

            foreach (var item in ben)
            {
                AutoSettle.Students.Add(item);
            }

            foreach (var item in noBen)
            {
                AutoSettle.Students.Add(item);
            }

            AutoSettle.SetRatings();
            AutoSettle.OrderStudentsByRating();
        }

        private void GetRooms()
        {
            AutoSettle.FreeRooms = new List<SettleRoom>();

            var allRooms = new List<SettleRoom>();
            var settledRooms = new List<SettleRoom>();
            var settleStudents = new List<SettleStudent>();

            var rooms = _repository.Get<tblRoom>();
            var studentRooms = _repository.Get<tblStudentRoom>();
            var hostels = _repository.Get<tblHostel>();
            var students = _repository.Get<tblStudent>();

            var all = from r in rooms
                      join h in hostels on r.HostelId equals h.Id
                      select new SettleRoom(r.Id, r.Number, h.Number, r.AmountPlaces, new List<SettleStudent>());

            var studs = from sr in studentRooms
                        join s in students on sr.StudentId equals s.Id
                        select new SettleStudent(s.Id, s.GenderType);

            var settled = from r in rooms
                          join sr in studentRooms on r.Id equals sr.RoomId
                          join s in settleStudents on sr.StudentId equals s.Id
                          join h in hostels on r.HostelId equals h.Id
                          group new { r, s, h } by r.Id into grouped
                          select new SettleRoom(
                              grouped.Key,
                              grouped.Select(g => g.r.Number).FirstOrDefault(),
                              grouped.Select(g => g.h.Number).FirstOrDefault(),
                              grouped.Select(g => g.r.AmountPlaces).FirstOrDefault(),
                              grouped.Select(g => g.s).ToList()
                              //new List<SettleStudent>()
                              );
 
            foreach (var item in studs)
            {
                settleStudents.Add(item);
            }

            foreach (var item in settled)
            {
                settledRooms.Add(item);
            }

            foreach (var item in all)
            {
                allRooms.Add(item);
            }

            //foreach (var item in settled)
            //{
            //    settledRooms.Add(item);
            //}

            for (var i = 0; i < settledRooms.Count; i++)
            {
                for (var j = 0; j < allRooms.Count; j++)
                {
                    if (settledRooms[i].Id == allRooms[j].Id)
                    {
                        allRooms[j] = settledRooms[i];
                        break;
                    }
                }
            }

            AutoSettle.FreeRooms = allRooms;
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