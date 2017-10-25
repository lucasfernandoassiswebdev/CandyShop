<<<<<<< HEAD
﻿using System.Net;
using System.Web.Mvc;
=======
﻿using System.Web.Mvc;
>>>>>>> 75723c4e02f86d21f37661cecd434f95e9872ee9


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

<<<<<<< HEAD
            if (filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            else
                filterContext.Result = new RedirectResult("/Home");
=======
            filterContext.Result = new RedirectResult("/CandyShop");
>>>>>>> 75723c4e02f86d21f37661cecd434f95e9872ee9
        }
    }
}