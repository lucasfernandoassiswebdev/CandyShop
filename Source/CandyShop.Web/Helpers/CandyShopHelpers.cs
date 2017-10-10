using System;
using System.Web.Mvc;

namespace CandyShop.Web.Helpers
{
    public static class CandyShopHelpers
    {
        public static string UrlContent(this HtmlHelper h, params object[] str)
        {
            return $"~/{string.Join("/", str)}?d={DateTime.Now.Ticks}";
        }
    }
}
