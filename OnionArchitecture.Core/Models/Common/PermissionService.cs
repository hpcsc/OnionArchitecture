using System.Collections.Generic;
using System.Linq;

namespace OnionArchitecture.Core.Models.Common
{
    public class PermissionService
    {
        public static List<Permission> MergePermissions(IList<Permission> permissions)
        {
            var permissionLookUp = new Dictionary<int, Permission>();

            foreach (var permission in permissions)
            {
                if (permissionLookUp.ContainsKey(permission.ResourceId))
                {
                    MergeIntoFirstPermission(permissionLookUp[permission.ResourceId], permission);
                }
                else
                {
                    permissionLookUp[permission.ResourceId] = permission;
                }
            }

            return permissionLookUp.Values.ToList();
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
