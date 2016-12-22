using Autofac;
using FluentValidation;
using OnionArchitecture.Core.Infrastructure.Caching;
using OnionArchitecture.Core.Infrastructure.Settings;
using OnionArchitecture.Infrastructure.Caching;
using OnionArchitecture.Infrastructure.Logging;
using OnionArchitecture.Infrastructure.Settings;
using OnionArchitecture.Infrastructure.Validation;
using OnionLogging = OnionArchitecture.Core.Infrastructure.Logging;

namespace OnionArchitecture.Infrastructure
{
    public class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<WebConfigApplicationSettings>().As<IApplicationSettings>().SingleInstance();
            builder.RegisterType<NLogAdapter>().As<OnionLogging.ILogger>().SingleInstance();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().InstancePerRequest();
            builder.RegisterType<HttpContextCacheAdapter>().As<ICacheStore>().SingleInstance();
        }        
    }
}
