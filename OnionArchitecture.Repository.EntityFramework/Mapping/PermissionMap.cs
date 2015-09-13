using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OnionArchitecture.Repository.EntityFramework.PersistenceModel;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    internal class PermissionMap : EntityTypeConfiguration<PermissionPersistenceModel>
    {
        public PermissionMap()
        {
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
        }
    }
}
