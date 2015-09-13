using OnionArchitecture.Repository.EntityFramework.Mapping;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OnionArchitecture.Repository.EntityFramework
{
    public class OnionArchDbContext : DbContext
    {
        static OnionArchDbContext()
        {
            Database.SetInitializer<OnionArchDbContext>(null);
        }

        public OnionArchDbContext(string connectionStringName)
            : base(connectionStringName)
        {
            base.Configuration.LazyLoadingEnabled = false;
        }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new ResourceMap());            
        }
    }
}
