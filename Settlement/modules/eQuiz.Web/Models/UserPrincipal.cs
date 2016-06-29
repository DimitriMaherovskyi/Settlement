using System.Security.Principal;
using System.Linq;
using System.Collections.Generic;

namespace Settlement.Web.Models
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            List<string> roles = role.Split(',').ToList<string>();
            if (roles.Contains(RoleName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
    }
}