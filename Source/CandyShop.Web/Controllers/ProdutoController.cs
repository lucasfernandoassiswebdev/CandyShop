using CandyShop.Application;
using CandyShop.Application.ViewModels;
using System.Net;
using System.Web.Mvc;


namespace CandyShop.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoApplication _appProduto = new ProdutoApplication();

        // GET: Produto
        public ActionResult Index()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)
            {                
                return Content("Erro " + response.ContentAsString);
            }
            return View(response.Content);
        }

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            var response = _appProduto.InserirProduto(produto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Content("Erro " + response.StatusCode);
            }            
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
        public ActionResult EditarProduto(Produto produto)
        {
            return Content("Produto editado com sucesso!");
        }

        public ActionResult ExcluirProduto(/*int idProduto*/)
        {
            return View();
        }

        public ActionResult ExcluirProdutoConfirmado(int idProduto)
        {
            return Content("Produto excluído com sucesso!");
        }
    }
}