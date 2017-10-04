using CandyShop.Application.Interfaces;
using CandyShop.Web.Filters;
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
    }
}