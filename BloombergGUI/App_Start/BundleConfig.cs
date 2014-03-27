using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BloombergGUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/infragisticsgrid").Include(
                       "~/scripts/infragistics/js/infragistics.core.js",
                       "~/scripts/infragistics/js/infragistics.lob.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                       "~/scripts/foundation/foundation.js",
                       "~/scripts/foundation/foundation.topbar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/Infragistics/css/structure/bundle").Include(
              "~/Content/Infragistics/css/structure/infragistics.css"));
            bundles.Add(new StyleBundle("~/Content/Infragistics/css/themes/metro/bundle").Include(
                "~/Content/Infragistics/css/themes/metro/infragistics.theme.css"));
            //bundles.Add(new StyleBundle("~/Content/Infragistics/css/themes/infragistics/bundle").Include(
            //    "~/Content/Infragistics/css/themes/infragistics/infragistics.theme.css"));
            bundles.Add(new StyleBundle("~/Content/grid").Include(
                "~/Content/grid.css"));



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

            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                     "~/Content/foundation/normalize.css",
                     "~/Content/foundation/foundation.css",
                     "~/Content/foundation/foundation-icons.css"));
        }
    }
}