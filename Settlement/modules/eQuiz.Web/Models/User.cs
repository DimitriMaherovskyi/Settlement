using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? Quote { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(int id, string username, string password, string email, int roleId, DateTime createdDate, DateTime? lastLogin, int? quote, string firstName, string lastName)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            RoleId = roleId;
            CreatedDate = createdDate;
            LastLoginDate = lastLogin;
            Quote = quote;
            FirstName = firstName;
            LastName = LastName;
        }

        public User()
        {

        }
    }
}