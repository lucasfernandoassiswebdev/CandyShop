using System.Web.Mvc;
using System.Web.Routing;

namespace CandyShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Administracao",
                url: "Administracao",
                defaults: new { controller = "Admin", action = "Index" }
            );
            routes.MapRoute(
                name: "CandyShop",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "NavBar"}
            );
            
        }
    }
}
