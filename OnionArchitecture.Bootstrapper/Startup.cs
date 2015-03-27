using Microsoft.Owin;
using OnionArchitecture.Bootstrapper.DependencyResolution;
using Owin;

[assembly: OwinStartup(typeof(OnionArchitecture.Bootstrapper.Startup))]
namespace OnionArchitecture.Bootstrapper
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyConfig.RegisterDependencies();
            AutoMapperConfig.SetupBindings();

            //To fix the issue EntityFramework.SqlServer.dll is not copied to Web bin because there's no code referencing this dll explicitly
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
