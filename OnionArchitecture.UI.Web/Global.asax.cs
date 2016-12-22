using Autofac;
using Autofac.Integration.Mvc;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.UI.Web.Models;
using Sharpenter.BootstrapperLoader;
using Sharpenter.BootstrapperLoader.Builder;
using Sharpenter.BootstrapperLoader.Helpers;
using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace OnionArchitecture.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var bootstrapperLoader = new LoaderBuilder()
                .Use(new FileSystemAssemblyProvider(HttpRuntime.BinDirectory, "OnionArchitecture.*.dll"))
                .Build();

            var container = ConfigureIoC(bootstrapperLoader);
            bootstrapperLoader.TriggerConfigure(container.Resolve);
        }

        private static IContainer ConfigureIoC(BootstrapperLoader bootstrapperLoader)
        {
            //Create a container builder
            var builder = new ContainerBuilder();
            //Register all controllers in current assembly with container builder            
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            bootstrapperLoader.Trigger("ConfigureContainer", builder);
            
            //Build a real container from container builder
            var container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var deserializer = new JavaScriptSerializer();

                var serializedModel = deserializer.Deserialize<CustomPrincipalSerializationModel>(authTicket.UserData);

                if (serializedModel != null)
                {
                    var customPrincipal = new CustomPrincipal(serializedModel.Id, 
                                        serializedModel.Username, 
                                        serializedModel.FullName,
                                        serializedModel.Roles);

                    HttpContext.Current.User = customPrincipal;
                }
            }
        } 
    }
}
