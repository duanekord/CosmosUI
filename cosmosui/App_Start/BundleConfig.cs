using System.Web;
using System.Web.Optimization;

namespace cosmosui
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery.validate*",
            "~/Scripts/modernizr-*",
            "~/Scripts/bootstrap.js",
            "~/Scripts/kendo/kendo.all.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/app").Include(
              "~/Content/bootstrap.css",
              "~/Content/site.css",
              "~/Content/kendo/kendo.common.min.css",
            "~/Content/kendo/kendo.default.min.css"));


            bundles.IgnoreList.Clear();

        }
    }
}
