using CandyShop.Repository.DataBase;
using CandyShop.Repository.Repositorys;
using CandyShop.WebAPI;
using CandyShop.WebAPI.Filtros;
using CandyShop.WebAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector.Integration.WebApi;
using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace CandyShop.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilter());

            // Definindo rotas padrões na API
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Configurando o container de injeção de dependência do simple injector
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.Build());

            /* Adicionando o filtro de exceções a todos os Controllers, dessa forma
               o erro pode ser tratado por ele independente do controller ou da action
               em que ocorrer */
            config.Filters.Add(new ExceptionFilter());

            // Aplicando as configurações do cors
            ConfigureCors(app);

            // Ativando cors
            app.UseCors(CorsOptions.AllowAll);

            AtivandoAcessTokens(app);

            // Ativando configuração WebApi
            app.UseWebApi(config);
        }

        private static void AtivandoAcessTokens(IAppBuilder app)
        {
            var conexao = new Conexao();
            var repositorio = new UsuarioRepository(conexao);
            // Configurando o fornecimento de tokens
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ProviderDeTokensDeAcesso(repositorio)
            };

            // Ativando o uso de acess tokens 
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private static void ConfigureCors(IAppBuilder app)
        {
            var politica = new CorsPolicy { AllowAnyHeader = true };
            politica.Origins.Add("http://localhost:40874");
            politica.Origins.Add("http://localhost:40880");

            politica.Methods.Add("GET");
            politica.Methods.Add("POST");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(politica)
                }
            };
            app.UseCors(corsOptions);
        }
    }
}