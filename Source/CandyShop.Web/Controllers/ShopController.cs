using CandyShop.Application.Interfaces;
using CandyShop.Web.Filters;
using System.Net;
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
            var response = _appUsuario.VerificaCreditoLoja();
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro ao calcular saldo da lojinha. {response.ContentAsString}");

            ViewBag.SaldoAtual = "Saldo atual da loja: R$" + response.Content;
            return View();
        }
    }
}