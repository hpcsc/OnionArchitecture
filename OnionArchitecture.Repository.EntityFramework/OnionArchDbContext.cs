using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using OnionArchitecture.Repository.EntityFramework.Mapping;

namespace OnionArchitecture.Repository.EntityFramework
{
    public class OnionArchDbContext : DbContext, IDbContext
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

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int Commit()
        {
            return base.SaveChanges();
        }

        public new DbEntityEntry Entry<T>(T entity) where T : class
        {
            return base.Entry(entity);
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
