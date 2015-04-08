
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace OnionArchitecture.Core.Models.Common
{
    public class User : EntityBase, IAuthorizable
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

        public bool HasAccessTo(int resourceId, PermissionType type)
        {
            if(Permissions == null)
            {
                throw new SecurityException("User permissions are not loaded");
            }

            if(HasDenyPermissionTo(resourceId))
            {
                return false;
            }

            var userResourcePermission = Permissions.FirstOrDefault(p => p.ResourceId == resourceId);
            if (userResourcePermission != null && (userResourcePermission.Type & type) == type)
            {
                return true;
            }

            foreach (var role in Roles)
            {
                if (role.HasAccessTo(resourceId, type))
                {
                    return true;
                }
            }

            return false;
        }


        public bool HasDenyPermissionTo(int resourceId)
        {
            if (Permissions == null)
            {
                throw new SecurityException("User permissions are not loaded");
            }

            var resourcePermission = Permissions.FirstOrDefault(p => p.ResourceId == resourceId);

            if(resourcePermission != null && ((resourcePermission.Type & PermissionType.Deny) == PermissionType.Deny))
            {
                return true;
            }

            foreach (var role in Roles)
            {
                if (role.HasDenyPermissionTo(resourceId))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
