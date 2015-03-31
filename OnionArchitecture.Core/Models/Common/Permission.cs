
namespace OnionArchitecture.Core.Models.Common
{
    public class Permission : EntityBase
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int ResourceId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
        public Resource Resource { get; set; }
        public PermissionType Type { get; set; }
    }
}
