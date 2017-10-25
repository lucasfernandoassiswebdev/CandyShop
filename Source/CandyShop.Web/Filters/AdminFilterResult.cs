﻿using System.Web.Mvc;


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

            filterContext.Result = new RedirectResult("/CandyShop");
        }
    }
}