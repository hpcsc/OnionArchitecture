using System.Web.Mvc;
using System.Web.Routing;
using OnionArchitecture.UI.Web.Helpers.Alerts;

namespace OnionArchitecture.UI.Web.Helpers
{
    public class ValidateModelState :  ActionFilterAttribute, IActionFilter
    {
        public ValidateModelState()
        {
            ActionToRedirectIfInvalid = "List";
            ForAjaxRequest = false;
        }

        public string ControllerToRedirectIfInvalid { get; set; }
        public string ActionToRedirectIfInvalid { get; set; }
        public bool ForAjaxRequest { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                if (ForAjaxRequest)
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            Success = false,
                            Errors = modelState.GetModelStateErrorsAsList()
                        }
                    };
                }
                else
                {
                    var controllerToRedirect = string.IsNullOrWhiteSpace(ControllerToRedirectIfInvalid)
                    ? filterContext.RouteData.Values["controller"].ToString()
                    : ControllerToRedirectIfInvalid;

                    var errors = modelState.GetModelStateErrorsAsHtml();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", controllerToRedirect},
                        {"action", ActionToRedirectIfInvalid}
                    }).WithError(errors);                                    
                }                
            }
        }
    }
}