using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Settlement.Entities;
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
            var student = _repository.GetSingle<Student>(s => s.Id == id);
            var studentRoom = _repository.GetSingle<StudentRoom>(sr => sr.StudentId == id);

            var room = _repository.GetSingle<Entities.Room>(r => r.Id == studentRoom.RoomId);
            var hostel = _repository.GetSingle<Hostel>(h => h.Id == room.HostelId);

            var studentViolations = _repository.Get<StudentViolation>(sv => sv.StudentId == id);
            var violations = _repository.Get<Entities.Violation>();

            var query = from v in violations
                        join sv in studentViolations on v.Id equals sv.ViolationId
                        select new Models.Violation(v.Id, v.Name, v.Penalty);

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
            var violations = _repository.Get<Entities.Violation>();

            foreach(var item in violations)
            {
                result.Add(new Models.Violation(item.Id, item.Name, item.Penalty));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetHostels()
        {
            var result = new List<Test>();
            var hostels = _repository.Get<Hostel>();

            foreach (var item in hostels)
            {
                var t = new Test(item.Id);
                result.Add(t);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRooms()
        {
            var result = new List<object>();
            var rooms = _repository.Get<Entities.Room>();

            foreach (var item in rooms)
            {
                result.Add(new Models.Room(item.Id, item.Number, item.AmountPlaces, item.RoomFloor));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods

        [HttpPost]
        public void SaveStudentProfileInfo(int id, string name, string surname, string studyGroup, string institute)
        {
            var student = _repository.GetSingle<Student>(s => s.Id == id);

            student.Firstname = name;
            student.Surname = surname;
            student.StudyGroup = studyGroup;
            student.Insitute = institute;

            _repository.Update<Student>(student);
        }

        [HttpPost]
        public void AddViolation(int studentId, int violationId, DateTime time)
        {
            var violation = new StudentViolation();
            violation.StudentId = studentId;
            violation.ViolationId = violationId;
            violation.Time = time;

            _repository.Insert<StudentViolation>(violation);
        }

        //To do tomorrow
        [HttpPost]
        public void AddPayment()
        {

        }

        #endregion
    }
}