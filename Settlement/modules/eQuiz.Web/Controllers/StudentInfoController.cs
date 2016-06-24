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
    public class StudentInfoController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public StudentInfoController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web Actions

        [HttpGet]
        public JsonResult GetStudentInfo(int id)
        {
            var student = _repository.GetSingle<tblStudent>(s => s.Id == id);
            var studentRoom = _repository.GetSingle<tblStudentRoom>(sr => sr.StudentId == id);

            var room = _repository.GetSingle<tblRoom>(r => r.Id == studentRoom.RoomId);
            var hostel = _repository.GetSingle<tblHostel>(h => h.Id == room.HostelId);

            var studentViolations = _repository.Get<tblStudentViolation>(sv => sv.StudentId == id);
            var violations = _repository.Get<tblViolation>();

            var query = from v in violations
                        join sv in studentViolations on v.Id equals sv.ViolationId
                        select new Violation(v.Id, v.Name, v.Penalty);

            var vv = new List<object>();

            foreach (var item in query)
            {
                vv.Add(item);
            }

            var result = new StudentInfo(id, student.Firstname, student.Surname, student.Insitute, student.StudyGroup, studentRoom.DateOut, room.Number, hostel.Number, vv);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetViolationsList()
        {
            var result = new List<object>();
            var violations = _repository.Get<tblViolation>();

            foreach(var item in violations)
            {
                result.Add(new Violation(item.Id, item.Name, item.Penalty));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetHostels()
        {
            var result = new List<Hostel>();
            var hostels = _repository.Get<tblHostel>();

            foreach (var item in hostels)
            {
                var t = new Models.Hostel(item.Id, item.Number, item.Address, item.MonthPaymentSum);
                result.Add(t);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRooms()
        {
            var result = new List<Room>();
            var rooms = _repository.Get<tblRoom>();

            foreach (var item in rooms)
            {
                result.Add(new Room(item.Id, item.Number, item.AmountPlaces, item.RoomFloor, item.HostelId));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void SaveStudentProfileInfo(int id, string name, string surname, string studyGroup, string institute)
        {
            var student = _repository.GetSingle<tblStudent>(s => s.Id == id);

            student.Firstname = name;
            student.Surname = surname;
            student.StudyGroup = studyGroup;
            student.Insitute = institute;

            _repository.Update<tblStudent>(student);
        }

        [HttpPost]
        public void AddViolation(int studentId, int violationId, DateTime time)
        {
            var violation = new tblStudentViolation();
            violation.StudentId = studentId;
            violation.ViolationId = violationId;
            violation.Time = time;

            _repository.Insert<tblStudentViolation>(violation);
        }

        //To do tomorrow
        [HttpPost]
        public void AddPayment()
        {

        }

        #endregion
    }
}