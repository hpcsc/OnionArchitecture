using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class DisplayUserPermissionModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<PermissionDTO> UserPermissions { get; set; }
        public IEnumerable<PermissionDTO> RolePermissions { get; set; }
    }
}
