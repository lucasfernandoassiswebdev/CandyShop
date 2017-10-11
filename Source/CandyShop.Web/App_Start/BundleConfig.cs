using System;
using System.Web.Optimization;

namespace CandyShop.Web
{
    public class BundleConfig
    {
        /* Essas configurações são usadas para renderizar todas as páginas de scripts 
           e folhas de estilo necessárias de uma só vez */
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/materialize.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      $"~/Content/materialize.min.css?d={DateTime.Now.Ticks}",
                      "~/Content/Principal/shop.css"));
        }
    }
}
