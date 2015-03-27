using System.Collections.Generic;
using System.Web.Mvc;

namespace OnionArchitecture.UI.Web.Helpers.Alerts
{
    public static class AlertExtension
    {
        private const string _alertKey = "_Alert";

        public static List<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if(!tempData.ContainsKey(_alertKey))
            {
                tempData[_alertKey] = new List<Alert>();
            }

            return (List<Alert>)tempData[_alertKey];
        }

        public static ActionResult WithSuccess(this ActionResult actionResult, string message)
        {
            return new AlertDecoratorResult(actionResult, "alert-success", message);
        }

        public static ActionResult WithInfo(this ActionResult actionResult, string message)
        {
            return new AlertDecoratorResult(actionResult, "alert-info", message);
        }

        public static ActionResult WithWarning(this ActionResult actionResult, string message)
        {
            return new AlertDecoratorResult(actionResult, "alert-warning", message);
        }

        public static ActionResult WithError(this ActionResult actionResult, string message)
        {
            return new AlertDecoratorResult(actionResult, "alert-danger", message);
        }
    }
}