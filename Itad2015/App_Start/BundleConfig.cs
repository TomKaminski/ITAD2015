using System.Web.Optimization;

namespace Itad2015
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/countdown/jquery.countdown.js",
                        "~/Scripts/countdown.js",
                        "~/Scripts/jquery.vegas.js",
                        "~/Scripts/jquery.easing.1.3.js",
                        "~/Scripts/custom-slideshow.js",
                        "~/Scripts/jquery.animateNumber.js",
                        "~/Scripts/alertApp.js",
                        "~/Scripts/circle-progress.js",
                        "~/Scripts/registerApp.js"));

            bundles.Add(new ScriptBundle("~/modernizr").Include("~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                         "~/Content/bootstrap/bootstrap.css",
                         "~/Content/font-awesome/font-awesome.css",
                         "~/Content/vegas/jquery.vegas.css",
                         "~/Content/countdown.css",
                         "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminCheckJs").Include(
                "~/Scripts/angular.js",
                "~/Areas/Admin/Scripts/adminApp.js",
                "~/Areas/Admin/Scripts/services/registeredPersonService.js",
                "~/Areas/Admin/Scripts/services/hubProxyService.js",
                "~/Areas/Admin/Scripts/filters/getByIdFilter.js",
                "~/Areas/Admin/Scripts/filters/getByEmailFilter.js",
                "~/Areas/Admin/Scripts/services/guestFilterService.js",
                "~/Areas/Admin/Scripts/controllers/baseGuestController.js",
                "~/Areas/Admin/Scripts/controllers/guestCheckAdminCtrl.js"
                ));
        }
    }
}