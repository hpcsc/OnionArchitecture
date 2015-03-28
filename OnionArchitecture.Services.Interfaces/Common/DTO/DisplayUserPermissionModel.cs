using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class DisplayUserPermissionModel
    {
        public string FullName { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<PermissionDTO> Permissions { get; set; }
    }
}
