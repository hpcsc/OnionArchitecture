using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{    
    public class RoleMap : EntityBaseMap<Role>
    {
        public RoleMap()
        {
            HasMany(c => c.Permissions).WithOptional().HasForeignKey(c => c.RoleId);
        }
    }
}
