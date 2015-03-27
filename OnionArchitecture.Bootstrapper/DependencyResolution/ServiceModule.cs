using Autofac;
using OnionArchitecture.Services.Common;
using OnionArchitecture.Services.Interfaces.Common;

namespace OnionArchitecture.Bootstrapper.DependencyResolution
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManagePermissionService>().As<IManagePermissionService>().InstancePerRequest();
            builder.RegisterType<AuthenticateService>().As<IAuthenticateService>().InstancePerRequest();
        }
    }
}
