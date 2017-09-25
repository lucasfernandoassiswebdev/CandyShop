using CandyShop.Application.Interfaces;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoApplication _appProduto;

        public HomeController(IProdutoApplication produto)
        {
            _appProduto = produto;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Padrao()
        {
            if (Session["Login"] == null)
                Session["Login"] = "off";
            return View();
        }

        public ActionResult Main()
        {
            TempData["caminhoImagensProdutos"] = "../../Imagens/Produtos";
            var produtos = _appProduto.ListarProdutos();
            return View(produtos.Content);
        }
    }
}