﻿using SimpleInjector.Integration.WebApi;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace CandyShop.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.Build());
        }
    }
}
