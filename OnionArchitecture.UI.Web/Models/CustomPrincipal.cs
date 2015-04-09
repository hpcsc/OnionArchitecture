using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace OnionArchitecture.UI.Web.Models
{
    public class CustomPrincipal : IPrincipal
    {
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string FullName { get; private set; }
        public List<string> Roles { get; set; }

        public CustomPrincipal(int userId,
                               string username,
                               string fullName,
                               List<string> roles)
        {
            UserId = userId;
            Username = username;
            FullName = fullName;
            Roles = roles;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Username); }
        }

        public bool IsInRole(string role)
        {
            return Roles.FirstOrDefault(r => r.Equals(role, StringComparison.InvariantCultureIgnoreCase)) != null;
        }
    }
}
