
using System.Collections.Generic;
namespace OnionArchitecture.Services.Interfaces.Common.DTO.Input
{
    public class UpdateUserRolesAndPermissionInputModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<PermissionDTO> UserPermissions { get; set; }
    }
}
