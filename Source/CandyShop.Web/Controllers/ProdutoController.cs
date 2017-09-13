using CandyShop.Core.Services.Produto.Dto;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoDto produto)
        {
            return Content("Produto inserido com sucesso!");
        }
    }
}