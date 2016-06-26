using System.Security.Principal;

namespace Settlement.Web.Models
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if(role == userRole)
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
        public string userRole { get; set; }
    }
}