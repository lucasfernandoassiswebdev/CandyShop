using System.Web.Mvc;

namespace CandyShop.Web.Filters
{
    public class UserFilterResult : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Login"] == null)
                filterContext.HttpContext.Session["Login"] = "off";

            var classificacao = filterContext.HttpContext.Session["classificacao"]?.ToString();

            if (!filterContext.HttpContext.Session["Login"].ToString().Equals("off"))
                if (classificacao == "U" || classificacao == "A")
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }

            filterContext.Result = new RedirectResult("/navbar");
        }
    }
}