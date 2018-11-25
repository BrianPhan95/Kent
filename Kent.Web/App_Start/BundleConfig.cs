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
            ///style
            bundles.Add(new StyleBundle("~/Content/admin-site").Include(
                     "~/Content/AdminLTE/css/adminlte.min.css",
                     "~/Content/AdminLTE/Plugins/iCheck/flat/blue.css",
                     "~/Content/AdminLTE/Plugins/morris/morris.css",
                     "~/Content/AdminLTE/Plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                     "~/Content/AdminLTE/Plugins/datepicker/datepicker3.css",
                     "~/Content/AdminLTE/Plugins/daterangepicker/daterangepicker-bs3.css0",
                     "~/Content/AdminLTE/Plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"));

            ///script
            bundles.Add(new ScriptBundle("~/scripts/admin-site").Include(
                      "~/Content/AdminLTE/Plugins/jquery/jquery.min.js",
                      "~/Content/AdminLTE/Plugins/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/AdminLTE/Plugins/daterangepicker/daterangepicker.js",
                      "~/Content/AdminLTE/Plugins/datepicker/bootstrap-datepicker.js",
                      "~/Content/AdminLTE/Plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/Content/AdminLTE/Plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/AdminLTE/Plugins/fastclick/fastclick.js",
                      "~/Content/AdminLTE/js/adminlte.js",
                      "~/Content/AdminLTE/js/demo.js"));

            //CKeditor
            bundles.Add(new ScriptBundle("~/scripts/ckeditor").Include(
                "~/Scripts/Plugins/ckeditor/ckeditor.js",
                "~/Scripts/Shared/Initialize/ckeditor.init.js"));

            //Plugin styles
            bundles.Add(new StyleBundle("~/styles/plugins")
                .Include("~/Content/Plugins/Bootstrap/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/FontAwesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/jqueryui/jquery-ui.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/colorbox.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/Multiselect/Select2/select2.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/Multiselect/BootstrapDuallistbox/bootstrap-duallistbox.min.css",
                    "~/Content/Plugins/jquery.gritter.min.css")
                .Include("~/Content/Plugins/fancybox/jquery.fancybox.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/Bootstrap/bootstrap-editable.css",
                    "~/Scripts/Plugins/Datetimepicker/css/bootstrap-datetimepicker.min.css"));

            //Plugin scripts
            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                        "~/Scripts/Plugins/x-editable/bootstrap-editable.min.js",
                        "~/Scripts/Plugins/x-editable/ace-editable.min.js",
                        "~/Scripts/Plugins/BootBox/bootbox.min.js",
                        "~/Scripts/Plugins/typeahead-bs2.min.js",
                        "~/Scripts/Plugins/colorbox.js",
                        "~/Scripts/Plugins/moment/moment.min.js",
                        "~/Scripts/Plugins/moment/moment-timezone-with-data.min.js",
                        "~/Scripts/Plugins/jquery.maskedinput.min.js",
                        "~/Scripts/Plugins/fancybox/jquery.fancybox.js",
                        "~/Scripts/Plugins/Datetimepicker/bootstrap-datetimepicker.min.js",
                        "~/Scripts/Plugins/Bootstrap/bootstrap-datepicker.js",
                        "~/Scripts/Plugins/Multiselect/Select2/select2.min.js",
                        "~/Scripts/Plugins/Multiselect/BootstrapDuallistbox/jquery.bootstrap-duallistbox.min.js",
                        "~/Scripts/Plugins/jquery.cookie.js",
                        "~/Scripts/Plugins/Timezone/jstz.js",
                        "~/Scripts/Plugins/spin.min.js",
                        "~/Scripts/Plugins/jquery.gritter.min.js",
                        "~/Scripts/Plugins/JavascriptToolbox/date.js"));

            //Ace styles
            bundles.Add(new StyleBundle("~/styles/ace")
                .Include("~/Content/Plugins/Ace/ace-fonts.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Plugins/Ace/ace.min.css",
                        "~/Content/Plugins/Ace/ace-rtl.min.css",
                        "~/Content/Plugins/Ace/ace-skins.min.css"));
            //Ace scripts
            bundles.Add(new ScriptBundle("~/js/ace").Include(
                        "~/Scripts/Plugins/Ace/ace-elements.min.js",
                        "~/Scripts/Plugins/Ace/ace.min.js"));

            //Ace mediabrowser
            bundles.Add(new StyleBundle("~/styles/mediabrowser")
                .Include("~/Content/Media/fileuploader.css",
                    "~/Content/Media/mediaBrowser.css"));

            //Jquery validation
            bundles.Add(new ScriptBundle("~/js/mediabrowser").Include(
                        "~/Scripts/Media/fileuploader.js",
                        "~/Scripts/Media/jquery.hotkeys.js",
                        "~/Scripts/Media/jquery.jstree.js"));

            //Jquery validation
            bundles.Add(new ScriptBundle("~/js/administrator").Include(
                "~/Scripts/Shared/Initialize/validation.init.js",
                "~/Scripts/Shared/Initialize/admin.init.js",
                "~/Scripts/Shared/Helpers/siteHelper.js",
                "~/Scripts/Shared/Helpers/stringExtension.js"));

            //Jquery validation
            bundles.Add(new ScriptBundle("~/js/validation").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
