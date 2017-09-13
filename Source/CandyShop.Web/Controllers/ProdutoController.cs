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

        public ActionResult DetalheProduto(/*int idProduto*/)
        {
            return View();
        }

        public ActionResult EditarProduto(/*int idProduto*/)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarProduto(ProdutoDto produto)
        {
            return Content("Produto editado com sucesso!");
        }

        public ActionResult ExcluirProduto(/*int idProduto*/)
        {
            return View();
        }

        public ActionResult ExcluirProdutoConfirmado(int idProduto)
        {
            return View();
        }
    }
}