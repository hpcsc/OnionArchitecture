using Autofac;
using Autofac.Integration.Mvc;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Infrastructure.Aspects;
using System.Reflection;
using System.Web.Mvc;

namespace OnionArchitecture.Bootstrapper.DependencyResolution
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            //Create a container builder
            var builder = new ContainerBuilder();
            //Register all controllers in current assembly with container builder
            builder.RegisterControllers(Assembly.Load("OnionArchitecture.UI.Web")).PropertiesAutowired();
            
            //Register bootstrapper module
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new RepositoryModule());            
            builder.RegisterModule(new ServiceModule());
            
            //Register module from config file if needed
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            //Build a real container from container builder
            var container = builder.Build();
            //Set current dependency resolver to Autofac dependency resolver with current container,
            //Autofac will take over generation of ASP.NET MVC controllers
            //and we will be able to use Current property to get current Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            SetupAspects();
        }

        private static void SetupAspects()
        {
            AuditActionAttribute.AuditRepositoryFactory = () => DependencyResolver.Current.GetService<IAuditRepository>();
            AuditActionAttribute.UnitOfWorkFactory = () => DependencyResolver.Current.GetService<IUnitOfWork>();
        }
    }
}
