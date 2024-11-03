using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace Site
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.replace-text.js"));

            var bundle = new ScriptBundle("~/bundles/jqueryval") { Orderer = new AsIsBundleOrderer() };

            bundle
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/jquery.validate-vsdoc.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/globalize/globalize.js")
                .Include("~/Scripts/jquery.validate.globalize.js");
            bundles.Add(bundle);

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/popper.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //     "~/Scripts/jquery-{version}.js",
            //     "~/Scripts/jquery.plugin.js"));

            //    //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    //            "~/Scripts/jquery.validate*"));

            //    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //      "~/Scripts/jquery.validate.js",
            //      "~/Scripts/jquery.validate.unobtrusive.js",
            //      "~/Scripts/globalize.js",
            //      "~/Scripts/jquery.validate.globalize.js"));


            //    bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
            //                "~/Scripts/globalize.0.1.3/globalize.js"));

            //    //// Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            //    //// pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new StyleBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-*"));

            //    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //      "~/Script/bootstrap.js", "~/Scripts/respond.js"));

            //    //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    //          "~/Scripts/bootstrap.js",
            //    //          "~/Scripts/respond.js"));

            //    //bundles.Add(new StyleBundle("~/Content/css").Include(
            //    //          "~/Content/bootstrap.css",
            //    //          "~/Content/site.css"));


        }
    }

    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
