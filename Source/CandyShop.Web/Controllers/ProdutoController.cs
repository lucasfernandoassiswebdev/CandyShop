using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;


namespace CandyShop.Web.Controllers
{
    [AdminFilterResult]
    public class ProdutoController : Controller
    {
        private readonly IProdutoApplication _appProduto;
        private readonly string _pathProduto;

        public ProdutoController(IProdutoApplication produto)
        {
            _appProduto = produto;
            _pathProduto = ImagensConfig.EnderecoImagens;
        }

        public ActionResult CadastrarProduto()
        {
            ViewBag.ImagemPadrao = _pathProduto;
            return View();
        }
        public ActionResult DetalheProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }
        public ActionResult EditarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            ViewBag.Tela = response.Content.Ativo;
            return View(response.Content);
        }
        public ActionResult DesativarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }

        public ActionResult ListaProdutos()
        {
            return View();
        }
        public ActionResult Listar()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Produtos Ativos";
            return View("ListaProdutos", response.Content);

        }
        public ActionResult ListarInativos(string token)
        {
            var response = _appProduto.ListarInativos(token);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro. {response.ContentAsString}");

            TempData["nomeLista"] = "Produtos Inativos";

            return View("ListaProdutos", response.Content);
        }
        public ActionResult ProcurarProduto(string nome)
        {
            var response = _appProduto.ProcurarProduto(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");

            TempData["nomeLista"] = "Produtos relacionados";
            return View("ListaProdutos", response.Content);
        }

        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoViewModel produto, string token)
        {
            if (produto.NomeProduto == null || produto.QtdeProduto == 0 ||
                produto.Categoria == null || produto.PrecoProduto == 0)
                return Content("Os campos não podem estar vazios ou zerados");

            var response = _appProduto.InserirProduto(produto,token);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro. {response.ContentAsString}" : response.Content);
        }
        [HttpPost]
        public ActionResult EditarProduto(ProdutoViewModel produto, string token)
        {
            if (produto.NomeProduto == null || produto.QtdeProduto < 0 ||
                produto.Categoria == null || produto.PrecoProduto == 0 || produto.Ativo == null)
                return Content("Os campos não podem estar vazios ou zerados");

            var response = _appProduto.EditarProduto(produto,token);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro. {response.ContentAsString}" : "Produto editado com sucesso!");
        }
        [HttpPost]
        public ActionResult DesativarProdutoConfirmado(ProdutoViewModel produto, string token)
        {
            var response = _appProduto.DesativarProduto(produto,token);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro: {response.Status}" : "Produto desativado com sucesso!");
        }
    }
}

