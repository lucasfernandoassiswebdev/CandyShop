using CandyShop.Application.Interfaces;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class ShopController : AuthController
    {
        private readonly IUsuarioApplication _appUsuario;

        public ShopController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Index()
        {
            ViewBag.SaldoAtual = "Saldo atual da loja: R$" + _appUsuario.VerificaCreditoLoja().Content; 
            return View();
        }
    }
}