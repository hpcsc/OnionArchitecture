using OnionArchitecture.UI.Web.Helpers.FrameworkExtensions;
using System.Web.Mvc;

namespace OnionArchitecture.UI.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ActionResult JsonNet(object data)
        {
            return new JsonNetResult { Data = data };
        }
    }
}