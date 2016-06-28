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
            tblRoom room;
            tblHostel hostel;
            StudentInfo result;

            var studentViolations = _repository.Get<tblStudentViolation>(sv => sv.StudentId == id);
            var violations = _repository.Get<tblViolation>();

            var query = from v in violations
                        join sv in studentViolations on v.Id equals sv.ViolationId
                        select new Violation(v.Id, v.Name, v.Penalty, sv.Time.HasValue ? sv.Time.ToString(): null);

            var vv = new List<Violation>();

            foreach (var item in query)
            {
                vv.Add(item);
            }

            if (studentRoom != null)
            {
                room = _repository.GetSingle<tblRoom>(r => r.Id == studentRoom.RoomId);
                hostel = _repository.GetSingle<tblHostel>(h => h.Id == room.HostelId);
                result = new StudentInfo(id, student.Firstname, student.Surname, student.Insitute, student.StudyGroup, studentRoom.DateOut.ToShortDateString(), room.Number, hostel.Number, vv);
            }
            else
            {
                result = new StudentInfo(id, student.Firstname, student.Surname, student.Insitute, student.StudyGroup, "", null, null, vv);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetViolationsList()
        {
            var result = new List<object>();
            var violations = _repository.Get<tblViolation>();

            foreach(var item in violations)
            {
                result.Add(new Violation(item.Id, item.Name, item.Penalty, null));
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
        public void AddViolation(int studentId, int violationId)
        {
            var violation = new tblStudentViolation();
            violation.StudentId = studentId;
            violation.ViolationId = violationId;
            violation.Time = DateTime.Now;

            _repository.Insert<tblStudentViolation>(violation);
        }

        //To do tomorrow
        [HttpPost]
        public void AddPayment(int sum, int studentId, int hostelId, string dateTill)
        {
            var payment = new tblPayment();
            payment.Amount = sum;
            payment.StudentId = studentId;
            payment.HostelId = hostelId;
            payment.PaymentDate = DateTime.Now;
            _repository.Insert<tblPayment>(payment);
            var studentRoom = _repository.GetSingle<tblStudentRoom>(sr => sr.StudentId == studentId);
            studentRoom.DateOut = DateTime.Parse(dateTill);

            _repository.Update<tblStudentRoom>(studentRoom);
        }

        [HttpPost]
        public void CheckIn(int studentId, int roomId)
        {
            var rooms = _repository.Get<tblStudentRoom>();
            var studentRoom = new tblStudentRoom();

            studentRoom.StudentId = studentId;
            studentRoom.RoomId = roomId;
            studentRoom.DateIn = DateTime.Now;
            studentRoom.DateOut = DateTime.Now;

            try
            {
                studentRoom.Id = rooms.Count + 1;
                _repository.Insert<tblStudentRoom>(studentRoom);
            }
            catch
            {
                for (var i = 1; i < rooms.Count + 1; i++)
                {
                    if (i != rooms[i].Id)
                    {
                        studentRoom.Id = i;
                        try
                        {
                            _repository.Insert<tblStudentRoom>(studentRoom);
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            _repository.Update<tblStudentRoom>(studentRoom);
        }

        [HttpPost]
        public void CheckOut(int studentId)
        {
            
        }

        #endregion
    }
}