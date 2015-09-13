using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.PersistenceModel
{
    internal class PermissionPersistenceModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int ResourceId { get; set; }        
        public PermissionType Type { get; set; }
    }
}
