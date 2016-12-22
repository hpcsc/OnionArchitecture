using Autofac;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Repository.EntityFramework.Common;

namespace OnionArchitecture.Repository.EntityFramework
{
    public class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<OnionArchDbContext>().As<IDbContext>().InstancePerLifetimeScope()
                .WithParameter("connectionStringName", "OnionArchConnection");
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.RegisterType<ResourceRepository>().As<IResourceRepository>().InstancePerRequest();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerRequest();
            builder.RegisterType<AuditRepository>().As<IAuditRepository>().InstancePerRequest();
        }
    }
}
