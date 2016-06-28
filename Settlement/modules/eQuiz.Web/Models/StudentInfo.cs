using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Institute { get; set; }
        public string Group { get; set; }
        public string LivingTill { get; set; }
        public int? Room { get; set; }
        public int? Hostel { get; set; }
        public List<object> Violations;

        public StudentInfo(int id, string name, string surname, string institute, string group, string livingTill, int? room, int? hostel, List<object> violations)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Institute = institute;
            Group = group;
            LivingTill = livingTill;
            Room = room;
            Hostel = hostel;
            Violations = violations;
        }
    }
}