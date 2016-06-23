using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Settlement.Web.Models;

namespace Settlement.Web.Models
{
    public class RoomStuds
    {
        private List<StudentsReview> students;

        public int Id { get; set; }
        public int Number { get; set; }
        public int AmountPlaces { get; set; }
        public int RoomFloor { get; set; }
        public int HostelId { get; set; }
        public List<StudentsReview> Students
        {
            get
            {
                return students;
            }

            set
            {
                students = value;
            }
        }

        public bool HasRoomProblems { get; set; }


        public RoomStuds(int id, int number, int amount, int floor, int hostelId, List<StudentsReview> students, bool hasProblems)
        {
            Id = id;
            Number = number;
            AmountPlaces = amount;
            RoomFloor = floor;
            HostelId = hostelId;
            Students = students;
            HasRoomProblems = hasProblems;
        }

    }
}