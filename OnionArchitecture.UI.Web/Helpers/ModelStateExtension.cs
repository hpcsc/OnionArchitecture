using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnionArchitecture.UI.Web.Helpers
{
    public static class ModelStateExtension
    {
        public static string GetModelStateErrorsAsHtml(this ModelStateDictionary modelState)
        {
            return string.Join("<br/>",
                modelState.Values.Where(v => v.Errors != null && v.Errors.Any())
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));            
        }

        public static IList<string> GetModelStateErrorsAsList(this ModelStateDictionary modelState)
        {
            return modelState.Values.Where(v => v.Errors != null && v.Errors.Any())
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }
}