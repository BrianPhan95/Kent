using System.Web;
using System.Web.Optimization;

namespace Kent.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admin-site").Include(
                     "~/Content/AdminLTE/css/adminlte.min.css",
                     "~/Content/Plugins/iCheck/flat/blue.css",
                     "~/Content/Plugins/morris/morris.css",
                     "~/Content/Plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                     "~/Content/Plugins/datepicker/datepicker3.css",
                     "~/Content/Plugins/daterangepicker/daterangepicker-bs3.css0",
                     "~/Content/Plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"));

            bundles.Add(new ScriptBundle("~/scripts/admin-site").Include(
                      "~/Content/Plugins/jquery/jquery.min.js",
                      "~/Content/Plugins/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/Plugins/daterangepicker/daterangepicker.js",
                      "~/Content/Plugins/datepicker/bootstrap-datepicker.js",
                      "~/Content/Plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/Content/Plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/Plugins/fastclick/fastclick.js",
                      "~/Content/AdminLTE/js/adminlte.js",
                      "~/Content/AdminLTE/js/demo.js"));

        }
    }
}
