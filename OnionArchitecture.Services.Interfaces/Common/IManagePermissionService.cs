using System.Collections.Generic;
using OnionArchitecture.Services.Interfaces.Common.DTO;

namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IManagePermissionService
    {
        IEnumerable<UserDTO> FindAllUsers();
    }
}
