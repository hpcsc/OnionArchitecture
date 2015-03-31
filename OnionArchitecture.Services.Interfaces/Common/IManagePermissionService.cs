using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IManagePermissionService
    {
        PermissionIndexModel CreateIndexModel();
        DisplayUserPermissionModel GetUserPermission(string username);
        DisplayResourceDetailModel GetResourceDetail(int resourceId);
        void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input);
    }
}
