using Autofac;
using OnionArchitecture.Core.Infrastructure.Logging;
using OnionArchitecture.Core.Infrastructure.Settings;
using OnionArchitecture.Infrastructure.Logging;
using OnionArchitecture.Infrastructure.Settings;

namespace OnionArchitecture.Bootstrapper.DependencyResolution
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebConfigApplicationSettings>().As<IApplicationSettings>().SingleInstance();
            builder.RegisterType<NLogAdapter>().As<ILogger>().SingleInstance();
        }
    }
}
