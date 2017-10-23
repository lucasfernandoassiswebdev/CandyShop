using System;
using System.IO;
using System.Web.Mvc;

namespace CandyShop.Web.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\CSLog";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var arquivo = path + $@"\{DateTime.Now:yyyyMMddHHmmss}.txt";
                using (var file = new StreamWriter(arquivo))
                {
                    var texto = filterContext.Exception.Source + Environment.NewLine + filterContext.Exception.Message;
                    file.WriteLine(texto);
                }
            }
            else
            {
                var arquivo = path + $@"\{DateTime.Now:yyyyMMddHHmmss}.txt";
                using (var file = new StreamWriter(arquivo))
                {
                    var texto = filterContext.Exception.Source + Environment.NewLine + filterContext.Exception.Message;
                    file.WriteLine(texto);
                }
            }
            base.OnException(filterContext);

        }
    }
}