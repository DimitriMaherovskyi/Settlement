﻿using Settlement.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eQuiz.Entities;
using Settlement.Web.Models;

namespace Settlement.Web.Controllers
{
    public class HostelRoomsController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors

        public HostelRoomsController(IRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Web actions

        [AuthorizeAccess(Roles = "Rector,Warden")]
        [HttpGet]
        public JsonResult GetHostelInfo(int id)
        {
            var h = _repository.GetSingle<tblHostel>(hh => hh.Id == id);

            var hostel = new Hostel(h.Id, h.Number, h.Address, h.MonthPaymentSum);

            return Json(hostel, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeAccess(Roles = "Rector,Warden")]
        [HttpGet]
        public JsonResult GetHostelRooms(int hostelId)
        {
            var setteledStudents = new List<StudentsReview>();
            var setteledRooms = new List<object>();

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
                           where r.HostelId == hostelId
                           select new StudentsReview(s.Id, s.Firstname + " " + s.Surname, h.Number, r.Number, s.Insitute, false);

            foreach (var item in setteled)
            {
                setteledStudents.Add(item);
            }

            var query = from r in rooms
                        join sr in studentRooms on r.Id equals sr.RoomId
                        join s in setteledStudents on sr.StudentId equals s.Id
                        group new { r, s } by r.Id into grouped
                        select new RoomStuds(
                            grouped.Key,
                            grouped.Select(g => g.r.Number).Single(),
                            grouped.Select(g => g.r.AmountPlaces).Single(),
                            grouped.Select(g => g.r.RoomFloor).Single(),
                            grouped.Select(g => g.r.HostelId).Single(),
                            grouped.Select(g => g.s).ToList(),
                            false
                            );

            foreach (var item in query)
            {
                setteledRooms.Add(item);
            }

            return Json(setteledRooms, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post methods
        [AuthorizeAccess(Roles = "Warden")]
        [HttpPost]
        public void UpdateHostelInfo(int id, int number, string address, int monthPayment)
        {
            var h = _repository.GetSingle<tblHostel>(hh => hh.Id == id);

            h.Number = number;
            h.Address = address;
            h.MonthPaymentSum = monthPayment;

            _repository.Update<tblHostel>(h);
        }

        #endregion
    }
}