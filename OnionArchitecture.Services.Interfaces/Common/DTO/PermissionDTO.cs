using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public PermissionType Type { get; set; }
    }
}
