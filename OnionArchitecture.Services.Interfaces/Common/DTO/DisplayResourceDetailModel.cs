
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;
namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class DisplayResourceDetailModel
    {
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }
        public List<DisplayResourceDetailPermissionModel> Permissions { get; set; }

    }

    public class DisplayResourceDetailPermissionModel
    {
        public int PermissionId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public PermissionType Permission { get; set; }
    }
}
