using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace CandyShop.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Configurando a injeção de dependencia
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.Build());
        }
    }
}
