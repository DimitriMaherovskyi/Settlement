﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}