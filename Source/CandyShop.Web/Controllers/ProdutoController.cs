using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Net;
using System.Web.Mvc;


namespace CandyShop.Web.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly IProdutoApplication _appProduto;

        public ProdutoController(IProdutoApplication produto)
        {
            _appProduto = produto;
        }
        
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
            if (response.Status != HttpStatusCode.OK) 
               return Content("Deu ruim!");
            return Content("Deu bom!!");
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