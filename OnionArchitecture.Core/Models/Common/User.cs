
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace OnionArchitecture.Core.Models.Common
{
    public class User : EntityBase<int>, IAuthorizable
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private string FullName { get; set; }        
        private UserStatus Status { get; set; }

        private IList<Role> Roles { get; set; }
        private IList<Permission> Permissions { get; set; }

        public UserSnapshot GetSnapshot()
        {
            return new UserSnapshot
            {
                Id = Id,
                UserName = UserName,
                Password = Password,
                FullName = FullName,
                Status = Status
            };
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
