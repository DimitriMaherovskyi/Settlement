using eQuiz.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eQuiz.Entities;

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
            var students = _repository.Get<Student>();
            var studentRooms = _repository.Get<StudentRoom>();
            var rooms = _repository.Get<Room>();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // Institute mentor method
        [HttpGet]
        public JsonResult GetStudentsByInstitute(string institute)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}