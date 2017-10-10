using System;
using System.IO;
using System.Web.Mvc;

namespace CandyShop.Web.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string path = @"\\192.168.7.11\wwwroot\CSLog";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var arquivo = path + $@"\{DateTime.Now:yyyyMMddHHmmss}.txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(arquivo))
                {
                    var texto = filterContext.Exception.Source + Environment.NewLine + filterContext.Exception.Message;
                    file.WriteLine(texto);
                }
            }
            else
            {
                var arquivo = path + $@"\{DateTime.Now:yyyyMMddHHmmss}.txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(arquivo))
                {
                    var texto = filterContext.Exception.Source + Environment.NewLine + filterContext.Exception.Message;
                    file.WriteLine(texto);
                }
            }
            base.OnException(filterContext);

        }
    }
}