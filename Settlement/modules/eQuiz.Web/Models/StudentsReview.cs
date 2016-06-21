using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class StudentsReview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HostelNum { get; set; }
        public int? Room { get; set; }
        public string Institute { get; set; }
        public bool? HasProblem { get; set; }

        public StudentsReview(int id, string name, int? hostelNumber, int? room, string institute, bool? hasProblem)
        {
            Id = id;
            Name = name;
            HostelNum = hostelNumber;
            Room = room;
            Institute = institute;
            HasProblem = hasProblem;
        }
    }
}