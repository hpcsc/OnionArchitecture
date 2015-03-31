using System.Web.Optimization;

namespace OnionArchitecture.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/permission").Include(
                      "~/Content/ng-table.min.css",
                      "~/Content/bootstrap-treeview.css"));

            bundles.Add(new ScriptBundle("~/bundles/permission").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/ng-table.min.js",
                      "~/Scripts/bootstrap-treeview.min.js",
                      "~/Scripts/app/permission/app.js",
                      "~/Scripts/app/permission/services/data-service.js",
                      "~/Scripts/app/permission/controllers/permission-ctrl.js",
                      "~/Scripts/app/permission/directives/resource-tree.js",
                      "~/Scripts/app/permission/directives/resource-detail.js"));
        }
    }
}
