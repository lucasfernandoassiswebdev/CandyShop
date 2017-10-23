using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace CandyShop.WebAPI.Filtros
{
    /* Essa classe serve para a manipulação de erros, é usada para tratar
       e retornar os erros que possam acontecer ao invés de implementar
       try catch em todas as actions da API */
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}