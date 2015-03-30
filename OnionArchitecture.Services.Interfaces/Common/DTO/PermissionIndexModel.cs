using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class PermissionIndexModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<ResourceDTO> Resources { get; set; }
    }
}
