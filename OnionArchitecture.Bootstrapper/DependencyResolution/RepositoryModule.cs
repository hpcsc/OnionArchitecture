using Autofac;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Repository.EntityFramework;
using OnionArchitecture.Repository.EntityFramework.Common;

namespace OnionArchitecture.Bootstrapper.DependencyResolution
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OnionArchDbContext>().As<IDbContext>().InstancePerLifetimeScope()
                .WithParameter("connectionStringName", "OnionArchConnection");
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
        }
    }
}
