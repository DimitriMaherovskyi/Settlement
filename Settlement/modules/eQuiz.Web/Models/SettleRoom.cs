using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class SettleRoom
    {
        private List<SettleStudent> students;

        public int Id { get; set; }
        public int Number { get; set; }
        public int Hostel { get; set; }
        public string Gender { get; set; }
        public int Places { get; set; }
        public int PlacesLeft { get; set; }
        public List<SettleStudent> Students
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

        public SettleRoom (int id, int number, int hostel, int places, List<SettleStudent> students)
        {
            Id = id;
            Number = number;
            Hostel = hostel;
            Places = places;
            Students = students;
            PlacesLeft = places - students.Count;
        }

        public static void SetGender (SettleRoom room)
        {
            bool isSimilar = true;
            string gender;

            if (room.Students.Count == 0)
            {
                return;
            }

            gender = room.Students[0].Gender;

            for (var i = 1; i < room.Students.Count; i++)
            {
                if (room.Students[i].Gender != gender)
                {
                    isSimilar = false;
                    break;
                }
            }

            if (isSimilar)
            {
                room.Gender = gender;
            }
        }
        
    }
}