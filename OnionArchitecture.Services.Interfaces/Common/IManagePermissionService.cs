using System.Collections.Generic;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IManagePermissionService
    {
        IEnumerable<UserDTO> FindAllUsers();
        void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input);
    }
}
