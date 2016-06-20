using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.Web.Models
{
    public class StudentsReview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HostemNum { get; set; }
        public int? Room { get; set; }
        public string Institute { get; set; }
        public bool? HasProblem { get; set; }
    }
}