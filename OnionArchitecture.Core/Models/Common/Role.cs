
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace OnionArchitecture.Core.Models.Common
{
    public class Role : EntityBase, IAuthorizable
    {
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public bool HasAccessTo(int resourceId, PermissionType type)
        {
            if (Permissions == null)
            {
                throw new SecurityException(string.Format("Role {0} permissions are not loaded", Name));
            }

            var resourcePermission = Permissions.FirstOrDefault(p => p.ResourceId == resourceId);
            return resourcePermission != null && ((resourcePermission.Type & type) == type);
        }

        public bool HasDenyPermissionTo(int resourceId)
        {
            if (Permissions == null)
            {
                throw new SecurityException(string.Format("Role {0} permissions are not loaded", Name));
            }

            var resourcePermission = Permissions.FirstOrDefault(p => p.ResourceId == resourceId);

            return resourcePermission != null && ((resourcePermission.Type & PermissionType.Deny) == PermissionType.Deny);
        }
    }
}
