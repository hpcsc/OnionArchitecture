using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IManagePermissionService
    {
        PermissionIndexModel CreateIndexModel();
        DisplayUserPermissionModel GetUserPermission(string username);
        DisplayResourceDetailModel GetResourceDetail(int resourceId);
        void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input);
        void UpdateResource(UpdateResourceInputModel input);
        void AddResource(AddResourceInputModel input);
        IEnumerable<UserDTO> SearchUser(string input);
    }
}
