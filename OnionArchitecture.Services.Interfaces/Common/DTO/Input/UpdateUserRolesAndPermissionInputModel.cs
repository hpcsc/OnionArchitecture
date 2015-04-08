
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;
namespace OnionArchitecture.Services.Interfaces.Common.DTO.Input
{
    public class UpdateUserRolesAndPermissionInputModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<UpdateUserPermissionInputModel> UserPermissions { get; set; }
    }

    public class UpdateUserPermissionInputModel
    {
        public int Id { get; set; }
        public PermissionType Type { get; set; }
    }
}
