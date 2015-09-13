using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OnionArchitecture.Repository.EntityFramework.PersistenceModel;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    internal class UserMap : EntityTypeConfiguration<UserPersistenceModel>
    {
        public UserMap()
        {
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(c => c.Roles).WithMany();
            HasMany(c => c.Permissions).WithOptional().HasForeignKey(c => c.UserId);
        }
    }
}
