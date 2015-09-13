using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OnionArchitecture.Repository.EntityFramework.PersistenceModel;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    internal class ResourceMap : EntityTypeConfiguration<ResourcePersistenceModel>
    {
        public ResourceMap()
        {
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(c => c.Children).WithOptional().HasForeignKey(c => c.ParentId);
            HasMany(c => c.Permissions).WithRequired().HasForeignKey(c => c.ResourceId);
        }
    }
}
