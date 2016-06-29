using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eQuiz.Entities;
using Settlement.Web.Models;

namespace Settlement.Web.Controllers
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

        [AuthorizeAccess(Roles = "Rector,Warden")]
        // Overall method
        [HttpGet]
        public JsonResult GetStudentsList()
        {
            var result = GetStudents();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeAccess(Roles = "Dean")]
        // Institute mentor method
        [HttpGet]
        public JsonResult GetStudentsByInstitute()
        {
            string currentUserName = HttpContext.User.Identity.Name;
            var user = _repository.Get<tblUsers>(u => u.UserName == currentUserName).FirstOrDefault();
            string institute = user.Institute;

            var all = GetStudents();
            var result = new List<StudentsReview>();

            foreach (var item in all)
            {
                if (item.Institute == institute)
                {
                    result.Add(item);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        List<StudentsReview> GetStudents()
        {
            var result = new List<StudentsReview>();

            var students = _repository.Get<tblStudent>();

            var studentRooms = _repository.Get<tblStudentRoom>();
            var rooms = _repository.Get<tblRoom>();
            var hostels = _repository.Get<tblHostel>();

            var studentBenefits = _repository.Get<tblStudentBenefit>();
            var benefits = _repository.Get<tblBenefit>();

            var studentViolations = _repository.Get<tblStudentViolation>();

            var setteled = from s in students
                           join sr in studentRooms on s.Id equals sr.StudentId
                           join r in rooms on sr.RoomId equals r.Id
                           join h in hostels on r.HostelId equals h.Id
                           select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, h.Number, r.Number, s.Insitute, false);

            var unsetteled = from s in students
                             join sr in studentRooms on s.Id equals sr.StudentId into g1
                             from sr in g1.DefaultIfEmpty()
                             where sr == null
                             select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, null, null, s.Insitute, false);

            // Adding hasProblem mark
            var setteledList = new List<StudentsReview>();
            var dateOut = new List<DateOut>();

            foreach (var item in setteled)
            {
                setteledList.Add(item);
            }

            var problemsQuery = from sl in setteledList
                                join sr in studentRooms on sl.Id equals sr.StudentId
                                select new DateOut(sr.StudentId, sr.DateOut);

            foreach (var item in problemsQuery)
            {
                dateOut.Add(item);
            }

            for (var i = 0; i < setteledList.Count; i++)
            {
                for (var j = 0; j < dateOut.Count; j++)
                {
                    if (setteledList[i].Id == dateOut[j].Id && dateOut[j].OutDate < DateTime.Now)
                    {
                        setteledList[i].HasProblem = true;
                        break;
                    }
                }
            }

            // Adding students to result
            foreach (var item in setteledList)
            {
                result.Add(item);
            }

            foreach (var item in unsetteled)
            {
                result.Add(item);
            }



            // Adding benefits mark
            var benefitsQuery = from r in result
                                join sb in studentBenefits on r.Id equals sb.StudentId
                                select sb;

            var bens = new List<tblStudentBenefit>();

            foreach (var item in benefitsQuery)
            {
                bens.Add(item);
            }

            for (var i = 0; i < result.Count; i++)
            {
                for (var j = 0; j < bens.Count; j++)
                {
                    if (result[i].Id == bens[j].StudentId)
                    {
                        result[i].Name += " (п)";
                        break;
                    }
                }
            }

            // Adding request mark
            var settleRequests = _repository.Get<tblSettleRequest>(sr => sr.Status == true);

            for (var i = 0; i < result.Count; i++)
            {
                for (var j = 0; j < settleRequests.Count; j++)
                {
                    if (result[i].Id == settleRequests[j].StudentId)
                    {
                        result[i].Name += " (з)";
                        break;
                    }
                }
            }

            return result;
        }
    }
}