using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Filters
{
    public class AdminFilterResult : UserFilterResult
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Login"] == null)
                filterContext.HttpContext.Session["Login"] = "off";

            if (!filterContext.HttpContext.Session["Login"].ToString().Equals("off"))
                if (filterContext.HttpContext.Session["classificacao"].ToString() == "A")
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            else
                filterContext.Result = new RedirectResult("/Home");
        }
    }
}