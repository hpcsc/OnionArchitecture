using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IPermissionRepository
    {
        IList<Permission> FindPermissionsForUser(User user);
        IList<Permission> FindPermissionsForRoles(IList<Role> roles);
        IList<Permission> FindPermissionsForResource(Resource resource);
    }
}
