using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public class UserPermissionContainer
    {
        public List<Permission> UserPermissions { get; private set; }
        public List<Permission> RolePermissions { get; private set; }

        public UserPermissionContainer(List<Permission> userPermissions, List<Permission> rolePermissions)
        {
            UserPermissions = userPermissions;
            RolePermissions = rolePermissions;
        }
    }
}
