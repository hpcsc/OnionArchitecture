using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    public class PermissionMap : EntityBaseMap<Permission>
    {
        public PermissionMap()
        {
            HasRequired(c => c.Resource).WithMany().HasForeignKey(c => c.ResourceId);
        }
    }
}
