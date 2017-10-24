using CandyShop.Application.Interfaces;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    [AdminFilterResult]
    public class ShopController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public ShopController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Index(string token)
        {
            var response = _appUsuario.VerificaCreditoLoja(token);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro ao calcular saldo da lojinha. {response.ContentAsString}");

            ViewBag.SaldoAtual = $"Saldo atual da loja: {response.Content:C}";
            return View();
        }
    }
}