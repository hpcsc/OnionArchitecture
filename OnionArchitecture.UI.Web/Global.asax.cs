using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.UI.Web.Models;
using System;
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
