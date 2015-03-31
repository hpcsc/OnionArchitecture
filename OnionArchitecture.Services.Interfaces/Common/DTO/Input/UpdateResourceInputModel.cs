using System.Collections.Generic;
using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Services.Interfaces.Common.DTO.Input
{
    public class UpdateResourceInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UpdateResourcePermissionInputModel> Permissions { get; set; }
    }

    public class UpdateResourcePermissionInputModel
    {
        public int Id { get; set; }
        public bool IsRole { get; set; }
        public PermissionType Permission { get; set; }
    }
}
