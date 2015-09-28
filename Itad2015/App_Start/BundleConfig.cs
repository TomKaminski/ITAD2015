using System.Web.Optimization;

namespace Itad2015
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/countdown/jquery.countdown.js",
                        "~/Scripts/countdown.js",
                        "~/Scripts/jquery.vegas.js",
                        "~/Scripts/jquery.easing.1.3.js",
                        "~/Scripts/custom-slideshow.js"));

            bundles.Add(new ScriptBundle("~/modernizr").Include("~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                         "~/Content/bootstrap/bootstrap.css",
                         "~/Content/font-awesome/font-awesome.css",
                         "~/Content/vegas/jquery.vegas.css",
                         "~/Content/countdown.css",
                         "~/Content/style.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}