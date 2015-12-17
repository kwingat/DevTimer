using System.Web.Optimization;

namespace DevTimer
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/transition.js",
                      "~/Scripts/collapse.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                "~/Scripts/bootstrap-table.js",
                "~/Scripts/App/formatColumn.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                "~/Scripts/chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/clockface.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                "~/Scripts/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/underscore.js",
                "~/Scripts/underscore.string.js",
                "~/Scripts/App/_mixins.js",
                "~/Scripts/App/alert.js",
                "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                "~/Scripts/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/bootstrap-responsive.min.css",
                      "~/Content/bootstrap-table.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/clockface.css",
                      "~/Content/style.css",
                      "~/Content/style-responsive.css",
                      "~/Content/light.css",
                      "~/Content/CSS/site.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/daterangepicker-bs3").Include(
                "~/Content/CSS/bootstrap2-3-1.min.css",
                "~/Content/CSS/daterangepicker-bs3.css"));
        }
    }
}
