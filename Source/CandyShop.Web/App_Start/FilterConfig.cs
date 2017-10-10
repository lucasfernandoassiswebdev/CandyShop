using CandyShop.Web.Filters;
using System.Web.Mvc;

namespace CandyShop.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            /* Adicionando o filtro para tratar os erros independente do controller 
               ou action do qual ele veio */
            filters.Add(new ExceptionFilter());
        }
    }
}
