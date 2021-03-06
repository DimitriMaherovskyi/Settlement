﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class Violation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Penalty { get; set; }

        public string Time { get; set; }

        public Violation(int id, string name, int penalty, string time)
        {
            Id = id;
            Name = name;
            Penalty = penalty;
            Time = time;
        }
    }
}