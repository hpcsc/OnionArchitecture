using System.Collections.Generic;
using System.Linq;

namespace OnionArchitecture.Core.Models.Common
{
    public class PermissionService
    {
        public static UserPermissionContainer MergePermissions(List<Permission> permissions)
        {
            var userPermissions = new Dictionary<int, Permission>();
            var rolePermissions = new Dictionary<int, Permission>();

            foreach (var permission in permissions)
            {
                if(permission.UserId.HasValue)
                {
                    if(userPermissions.ContainsKey(permission.UserId.Value))
                    {
                        MergeIntoFirstPermission(userPermissions[permission.UserId.Value], permission);
                    }
                    else
                    {
                        userPermissions[permission.Id] = permission;
                    }
                }
                else
                {
                    if (rolePermissions.ContainsKey(permission.RoleId.Value))
                    {
                        MergeIntoFirstPermission(rolePermissions[permission.RoleId.Value], permission);
                    }
                    else
                    {
                        rolePermissions[permission.Id] = permission;
                    }
                }
            }

            return new UserPermissionContainer(userPermissions.Values.ToList(), rolePermissions.Values.ToList());
        }

        private static void MergeIntoFirstPermission(Permission first, Permission second)
        {
            if((first.Type & PermissionType.Deny) == PermissionType.Deny ||
                (second.Type & PermissionType.Deny) == PermissionType.Deny)
            {
                first.Type = PermissionType.Deny;
            }
            else
            {
                first.Type |= second.Type;
            }
        }
    }
}
