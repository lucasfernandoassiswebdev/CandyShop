using CandyShop.Web.Filters;
using System;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AdminController : Controller 
    {
        [AdminFilterResult]
        public ActionResult Index()
        {            
            Session["TipoDeLogin"] = "Admin";
            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error: " + e.Message
            };
        }
    }
}