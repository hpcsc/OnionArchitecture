using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using OnionArchitecture.UI.Web.Helpers.Alerts;

namespace OnionArchitecture.UI.Web.Helpers
{
    public class HandleBusinessException : HandleErrorAttribute
    {
        public HandleBusinessException()
        {
            ActionToRedirectIfInvalid = "Index";
            ForAjaxRequest = false;
        }

        public string ControllerToRedirectIfInvalid { get; set; }
        public string ActionToRedirectIfInvalid { get; set; }
        public bool ForAjaxRequest { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is FluentValidation.ValidationException)
            {
                var validationException = filterContext.Exception as FluentValidation.ValidationException;
                if (ForAjaxRequest)
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            Success = false,
                            Errors = validationException.Errors.Select(e => e.ErrorMessage)
                        }
                    };
                }
                else
                {
                    var controllerToRedirect = string.IsNullOrWhiteSpace(ControllerToRedirectIfInvalid)
                    ? filterContext.RouteData.Values["controller"].ToString()
                    : ControllerToRedirectIfInvalid;

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", controllerToRedirect},
                        {"action", ActionToRedirectIfInvalid}
                    }).WithError(string.Join("<br/>", validationException.Errors.Select(e => e.ErrorMessage)));
                }

                filterContext.ExceptionHandled = true;
            }
            else
            {
                var formParameters = filterContext.RequestContext.HttpContext.Request.Form;
                var queryStringParameters = filterContext.RequestContext.HttpContext.Request.QueryString;
                var routeParameters = filterContext.RouteData.Values;

                var parameters = new List<string>();
                parameters.AddRange(formParameters.Keys
                                            .Cast<string>()
                                            .Select(k => FormatParameterForLogging(k, formParameters[k])));
                parameters.AddRange(queryStringParameters.Keys
                                            .Cast<string>()
                                            .Select(k => FormatParameterForLogging(k, queryStringParameters[k])));
                parameters.AddRange(routeParameters.Select(p => FormatParameterForLogging(p.Key, p.Value)));


                //Logging of exceptions and parameters

                base.OnException(filterContext);
            }
        }

        private string FormatParameterForLogging(string key, object value)
        {
            return string.Format("[{0}: {1}]", key, JsonConvert.SerializeObject(value));
        }
    }
}