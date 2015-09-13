
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace OnionArchitecture.Core.Models.Common
{
    public class Role : EntityBase<int>, IAuthorizable
    {
        private string Name { get; set; }

        private IList<Permission> Permissions { get; set; }

        public RoleSnapshot GetSnapshot()
        {
            return new RoleSnapshot
            {
                Id = Id,
                Name = Name
            };
        }

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
