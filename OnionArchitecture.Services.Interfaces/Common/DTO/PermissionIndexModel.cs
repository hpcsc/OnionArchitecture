using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class PermissionIndexModel
    {
        public IEnumerable<ResourceDTO> Resources { get; set; }
        public IEnumerable<RoleDTO> AvailableRoles { get; set; }
    }
}
