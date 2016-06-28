using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string CreatedDate { get; set; }
        public string LastLoginDate { get; set; }
        public int? Quote { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(int id, string username, string email, int roleId, string createdDate, string lastLogin, int? quote, string firstName, string lastName)
        {
            UserId = id;
            Username = username;
            Email = email;
            RoleId = roleId;
            CreatedDate = createdDate;
            LastLoginDate = lastLogin;
            Quote = quote;
            FirstName = firstName;
            LastName = lastName;
        }

        public User()
        {

        }
    }
}