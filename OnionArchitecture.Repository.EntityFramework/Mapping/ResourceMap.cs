using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    public class ResourceMap : EntityBaseMap<Resource>
    {
        public ResourceMap()
        {
            HasMany(c => c.Children).WithOptional().HasForeignKey(c => c.ParentId);
        }
    }
}
