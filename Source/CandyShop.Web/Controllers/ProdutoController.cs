using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
            return Content(response.Status != HttpStatusCode.OK ? $"Erro. {response.ContentAsString}" : "Produto cadastrado com sucesso!!");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult EditarProduto(ProdutoViewModel produto)
        {
            var response = _appProduto.EditarProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro. {response.ContentAsString}");
            try
            {


                if (produto.ImagemA != null)
                {
                    string[] prefixos = {"data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64,"};
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemA.StartsWith(prefixo)) continue;
                        produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                        var bytes = Convert.FromBase64String(produto.ImagemA);

                        Image imagem = (Bitmap) new ImageConverter().ConvertFrom(bytes);

                        var caminho = $"{_pathProduto}/{produto.IdProduto}_A.jpg";

                        imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                    }
                }

                if (produto.ImagemB != null)
                {
                    string[] prefixos = {"data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64,"};
                    foreach (var prefixo in prefixos)
                        if (produto.ImagemB.StartsWith(prefixo))
                        {
                            produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                            var bytes = Convert.FromBase64String(produto.ImagemB);

                            Image imagem = (Bitmap) new ImageConverter().ConvertFrom(bytes);

                            var caminho = $"{_pathProduto}/{produto.IdProduto}_B.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }
                }

                if (produto.ImagemC != null)
                {
                    string[] prefixos = {"data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64,"};
                    foreach (var prefixo in prefixos)
                        if (produto.ImagemC.StartsWith(prefixo))
                        {
                            produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                            var bytes = Convert.FromBase64String(produto.ImagemC);

                            Image imagem = (Bitmap) new ImageConverter().ConvertFrom(bytes);

                            var caminho = $"{_pathProduto}/{produto.IdProduto}_C.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }
                }
            }
            catch
            {
                return Content("Erro ao salvar imagem");
            }
            if (produto.RemoverImagemA)
            {
                var filePath = Server.MapPath($"{_pathProduto}/{produto.IdProduto}_A.jpg");
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            if (produto.RemoverImagemB)
            {
                var filePath = Server.MapPath($"{_pathProduto}/{produto.IdProduto}_B.jpg");
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            if (produto.RemoverImagemC) return Content("Produto editado com sucesso!");
            {
                var filePath = Server.MapPath($"{_pathProduto}/{produto.IdProduto}_C.jpg");
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            return Content("Produto editado com sucesso!");
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

