using CandyShop.WebAPI.Filtros;
using SimpleInjector.Integration.WebApi;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace CandyShop.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            // Definindo rotas padrões na API
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.Build());

            /* Adicionando o filtro de exceções a todos os Controllers, dessa forma
               o erro pode ser tratado por ele independente do controller ou da action
               em que ocorrer */
            config.Filters.Add(new ExceptionFilter());
        }
    }
}
