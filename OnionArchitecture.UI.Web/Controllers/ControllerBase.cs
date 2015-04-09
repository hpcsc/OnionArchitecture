using OnionArchitecture.UI.Web.Helpers.FrameworkExtensions;
using OnionArchitecture.UI.Web.Models;
using System.Web.Mvc;

namespace OnionArchitecture.UI.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ActionResult JsonNet(object data)
        {
            return new JsonNetResult { Data = data };
        }

        protected CustomPrincipal CurrentUser
        {
            get
            {
                return User as CustomPrincipal;
            }
        }
    }
}