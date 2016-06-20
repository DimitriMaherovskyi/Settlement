using eQuiz.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eQuiz.Entities;
using eQuiz.Web.Models;

namespace eQuiz.Web.Controllers
{
    public class StudentReviewController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public StudentReviewController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web actions

        // Overall method
        [HttpGet]
        public JsonResult GetStudentsList()
        {
            var result = new List<StudentsReview>();

            var students = _repository.Get<Student>();

            var studentRooms = _repository.Get<StudentRoom>();
            var rooms = _repository.Get<Room>();
            var hostels = _repository.Get<Hostel>();

            var studentBenefits = _repository.Get<StudentBenefit>();
            var benefits = _repository.Get<Benefit>();

            var studentViolations = _repository.Get<StudentViolation>();

            var setteled = from s in students
                        join sr in studentRooms on s.Id equals sr.StudentId
                        join r in rooms on sr.RoomId equals r.Id
                        join h in hostels on r.HostelId equals h.Id
                        select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, h.Number, r.Number, s.Insitute, false);

            var unsetteled = from s in students
                             join sr in studentRooms on s.Id equals sr.StudentId into g1
                             from sr in g1.DefaultIfEmpty()
                             where sr == null
                             select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, null, null, s.Insitute, true);

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

        // Institute mentor method
        [HttpGet]
        public JsonResult GetStudentsByInstitute(string institute)
        {
            var result = new List<StudentsReview>();

            var students = _repository.Get<Student>();

            var studentRooms = _repository.Get<StudentRoom>();
            var rooms = _repository.Get<Room>();
            var hostels = _repository.Get<Hostel>();

            var studentBenefits = _repository.Get<StudentBenefit>();
            var benefits = _repository.Get<Benefit>();

            var studentViolations = _repository.Get<StudentViolation>();

            var setteled = from s in students
                           join sr in studentRooms on s.Id equals sr.StudentId
                           join r in rooms on sr.RoomId equals r.Id
                           join h in hostels on r.HostelId equals h.Id
                           where s.Insitute == institute
                           select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, h.Number, r.Number, s.Insitute, false);

            var unsetteled = from s in students
                             join sr in studentRooms on s.Id equals sr.StudentId into g1
                             from sr in g1.DefaultIfEmpty()
                             where sr == null && s.Insitute == institute
                             select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, null, null, s.Insitute, true);

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
    }
}