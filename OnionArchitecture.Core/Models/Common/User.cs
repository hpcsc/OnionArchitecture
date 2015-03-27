
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }        
        public UserStatus Status { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}
