using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OnionArchitecture.Repository.EntityFramework.PersistenceModel;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{    
    internal class RoleMap : EntityTypeConfiguration<RolePersistenceModel>
    {
        public RoleMap()
        {
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(c => c.Permissions).WithOptional().HasForeignKey(c => c.RoleId);
        }
    }
}
