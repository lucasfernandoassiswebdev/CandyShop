using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Linq;
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
        
        #region Telas
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        public ActionResult DetalheProduto(int idProduto)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            return View(response.Content);
        }

        public ActionResult EditarProduto(int idProduto)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            return View(response.Content);
        }

        public ActionResult DesativarProduto(int idProduto)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            return View(response.Content);
        }
        #endregion

        #region Listas
        public ActionResult Listar()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)            
                return Content("Erro " + response.ContentAsString.First());            
            return View("Index", response.Content);
        }

        public ActionResult ListarInativos()
        {
            var response = _appProduto.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro {response.ContentAsString.First()}");
            return View("Index", response.Content);
        }

        public ActionResult ProcurarProduto(string nome)
        {
            var response = _appProduto.ProcurarProduto(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            return View("Index", response.Content);
        }
        #endregion

        #region Execucoes
        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            var response = _appProduto.InserirProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content(response.ContentAsString);
            return Content("Produto cadastrado com sucesso!!");
        }

        [HttpPost]
        public ActionResult EditarProduto(Produto produto)
        {
            var response = _appProduto.EditarProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content(response.ContentAsString);
            return Content("Produto editado com sucesso!");
        }

        [HttpPost]
        public ActionResult DesativarProdutoConfirmado(int idProduto)
        {
            var response = _appProduto.DesativarProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            return Content("Produto desativado com sucesso!");
        }
        #endregion
    }
}