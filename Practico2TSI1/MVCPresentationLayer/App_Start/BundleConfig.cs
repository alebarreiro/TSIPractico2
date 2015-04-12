using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace MVCPresentationLayer
{
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<System.Web.Optimization.BundleFile> files)
        {
            return files;
        }
    }

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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquerychat").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.ui.chatbox.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.ui.chatbox.css",
                      "~/Content/jquery-ui.css"
                      ));

            var bund = new ScriptBundle("~/bundles/chat").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-ui.js",
                       "~/Scripts/jquery.ui.chatbox.js",
                       "~/Scripts/Chat.js"
                       );
            bund.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bund);
        }
    }
}
