using CandyShop.Application.Interfaces;
using CandyShop.Web.Filters;
using System;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public ShopController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        [AdminFilterResult]
        public ActionResult Index()
        {
            ViewBag.SaldoAtual = "Saldo atual da loja: R$" + _appUsuario.VerificaCreditoLoja().Content; 
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