using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CandyShop.Web.Helpers
{
    public static class CandyShopHelpers
    {
        public static string UrlContent(this HtmlHelper h, params object[] str)
        {
            return new UrlHelper(h.ViewContext.RequestContext).Content($"~/{string.Join("/", str)}?d={DateTime.Now.Ticks}");
        }
    }

    public static class Extensions
    {
        private static readonly char[] DefaultDelimeters = { ' ', '.', ',' };

        public static List<string> LastWord(this string stringValue)
        {
            return LastWord(stringValue, DefaultDelimeters);
        }

        private static List<string> LastWord(this string stringValue, char[] delimeters)
        {            
            var index = stringValue.LastIndexOfAny(delimeters);
            var ultima = stringValue.Substring(index).Trim();

            var regex = new Regex(@"dia");
            if (regex.Match(stringValue).Success)            
                return new List<string> {"Dia"};
            
            if (ultima.Equals("mês"))
                return new List<string> {""};
            if (ultima.Equals("semana"))
                return new List<string> { "Semana" };
            for (var i = 1; i < 13; i++)
                if (ultima.Equals($"{i}"))
                    return new List<string> { "Mes", ultima };
            return index > -1 ? new List<string> { ultima } : null;
        }
    }
}
