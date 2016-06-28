using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public Role(int id, string name)
        {
            RoleId = id;
            RoleName = name;
        }
    }
}