using OnionArchitecture.Core.Infrastructure.Auditing;
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO.Input
{
    public class UpdateResourceInputModel : IAuditable
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UpdateResourcePermissionInputModel> Permissions { get; set; }
    }

    public class UpdateResourcePermissionInputModel
    {
        public int PermissionId { get; set; }
        public int Id { get; set; }
        public bool IsRole { get; set; }
        public PermissionType Permission { get; set; }
    }
}
