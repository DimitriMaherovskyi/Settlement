using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Settlement.Entities;

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
            var result = new object();

            var student = _repository.GetSingle<Student>(s => s.Id == id);
            var studentRoom = _repository.GetSingle<StudentRoom>(sr => sr.StudentId == id);

            var room = _repository.GetSingle<Room>(r => r.Id == studentRoom.RoomId);
            var hostel = _repository.GetSingle<Hostel>(h => h.Id == room.HostelId);

            var studentViolations = _repository.Get<StudentViolation>(sv => sv.StudentId == id);
            var violations = _repository.Get<Violation>();

            var query = from v in violations
                        join sv in studentViolations on v.Id equals sv.ViolationId
                        select v;

            var vv = new List<object>();

            foreach (var item in query)
            {
                vv.Add(item);
            }

            result = new
            {
                Id = id,
                Name = student.Firstname,
                Surname = student.Surname,
                Institute = student.Insitute,
                Group = student.StudyGroup,
                LivingTill = studentRoom.DateOut,
                Room = room.Number,
                Hostel = hostel.Number,
                Violations = vv
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}