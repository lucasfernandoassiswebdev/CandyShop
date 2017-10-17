using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;


namespace CandyShop.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoApplication _appProduto;
        private readonly string _pathProduto;

        public ProdutoController(IProdutoApplication produto)
        {
            _appProduto = produto;
            _pathProduto = "Imagens/Produtos";
        }

        #region Telas
        [AdminFilterResult]
        public ActionResult ListaProdutos()
        {
            return View();
        }

        [AdminFilterResult]
        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [AdminFilterResult]
        public ActionResult DetalheProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            TempData["caminhoImagensProdutos"] = _pathProduto;
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }

        [AdminFilterResult]
        public ActionResult EditarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            ViewBag.Tela = response.Content.Ativo;
            TempData["caminhoImagensProdutos"] = _pathProduto;
            return View(response.Content);
        }

        [AdminFilterResult]
        public ActionResult DesativarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensProdutos"] = _pathProduto;
            return View(response.Content);
        }
        #endregion

        #region Listas
        [AdminFilterResult]
        public ActionResult Listar()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["caminhoImagensProdutos"] = _pathProduto;
            TempData["nomeLista"] = "Produtos Ativos";
            return View("ListaProdutos", response.Content);

        }

        [AdminFilterResult]
        public ActionResult ListarInativos()
        {
            var response = _appProduto.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro. {response.ContentAsString}");

            TempData["nomeLista"] = "Produtos Inativos";

            return View("ListaProdutos", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ProcurarProduto(string nome)
        {
            var response = _appProduto.ProcurarProduto(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");

            TempData["caminhoImagensProdutos"] = _pathProduto;
            TempData["nomeLista"] = "Produtos relacionados";
            return View("ListaProdutos", response.Content);
        }
        #endregion

        #region Execucoes
        [AdminFilterResult]
        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoViewModel produto)
        {
            var response = _appProduto.InserirProduto(produto);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro. {response.ContentAsString}" : response.Content);
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult EditarProduto(ProdutoViewModel produto)
        {
            var response = _appProduto.EditarProduto(produto);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro. {response.ContentAsString}" : "Produto editado com sucesso!");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult DesativarProdutoConfirmado(ProdutoViewModel produto)
        {
            var response = _appProduto.DesativarProduto(produto);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro: {response.Status}" : "Produto desativado com sucesso!");
        }
        #endregion

    }
}

