using System.Web.Mvc;

namespace CandyShop.Web.Filters
{
    public class UserFilterResult : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var classificacao = filterContext.HttpContext.Session["classificacao"].ToString();
            if (classificacao == "U" || classificacao == "A")
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            filterContext.Result = new RedirectResult("/Home/NavBar");
            
        }
    }
}