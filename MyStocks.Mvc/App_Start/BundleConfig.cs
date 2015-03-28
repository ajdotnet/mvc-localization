using AJ.Common;
using System.Web.Optimization;

namespace MyStocks.Mvc
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            Guard.AssertNotNull(bundles, "bundles");

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // globalization/localization
            // from https://github.com/jquery/globalize and below...
            // globalize.min.js version produced manually...
            // globalize.initialize.js written myself...
            bundles.Add(new ScriptBundle("~/bundles/globalization")
                .Include("~/Scripts/globalize.js")
                .Include("~/Scripts/globalize.culture.*")     // exclude if localization files are included on demand
                .Include("~/Scripts/globalize.initialize.js")
                );
 
            // manipulate exsting bundle to support validation... 
            var jqueryval = bundles.GetBundleFor("~/bundles/jqueryval");
            jqueryval.Include("~/Scripts/globalize.validation.js");

            // manipulate exsting bundle to support datepicker localization... 
            var jqueryui = bundles.GetBundleFor("~/bundles/jqueryui");
            jqueryui.Include(                           // option 0: exclude both if localization files are included on demand
                "~/Scripts/jquery-ui-i18n.js",          // option 1: from nuget package 
                //"~/Scripts/jquery.ui.datepicker-*",   // option 2: single localization files
                "~/Scripts/datepicker.initialize.js");

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}