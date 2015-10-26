using System.Web.Optimization;

namespace DevTimer
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                "~/Scripts/bootstrap-table.js",
                "~/Scripts/App/formatColumn.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/clockface.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                "~/Scripts/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/underscore.js",
                "~/Scripts/underscore.string.js",
                "~/Scripts/App/_mixins.js",
                "~/Scripts/App/alert.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/CSS/bootstrap.min.css",
                      "~/Content/CSS/bootstrap-responsive.min.css",
                      "~/Content/bootstrap-table.css",
                      "~/Content/CSS/font-awesome.min.css",
                      "~/Content/CSS/bootstrap-datepicker.css",
                      "~/Content/CSS/clockface.css",
                      //"~/Content/CSS/style-metro.css",
                      "~/Content/CSS/style.css",
                      "~/Content/CSS/style-responsive.css",
                      "~/Content/CSS/light.css",
                      "~/Content/CSS/site.css",
                      "~/Content/site.css"));
        }
    }
}
