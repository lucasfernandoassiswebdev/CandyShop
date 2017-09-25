﻿using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Drawing;
using System.Drawing.Imaging;
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

            TempData["caminhoImagens"] = "../../Imagens/Produtos";
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
        public ActionResult CadastrarProduto(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {

                var response = _appProduto.InserirProduto(produto);
                if (response.Status != HttpStatusCode.OK)
                    return Content(response.ContentAsString);

                var id = _appProduto.BuscaUltimoProduto();
                //salvando todas as imagens 
                if (produto.ImagemA != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (produto.ImagemA.StartsWith(prefixo))
                        {
                            produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                            byte[] bytes = System.Convert.FromBase64String(produto.ImagemA);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                            string caminho = $"~/Imagens/Produtos/{id.Content}_A.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                if (produto.ImagemB != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (produto.ImagemB.StartsWith(prefixo))
                        {
                            produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                            byte[] bytes = System.Convert.FromBase64String(produto.ImagemB);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                            string caminho = $"~/Imagens/Produtos/{id.Content}_B.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                if (produto.ImagemC != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (produto.ImagemC.StartsWith(prefixo))
                        {
                            produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                            byte[] bytes = System.Convert.FromBase64String(produto.ImagemC);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

                            string caminho = $"~/Imagens/Produtos/{id.Content}_C.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                return Content("Produto cadastrado com sucesso!!");
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPut]
        public ActionResult EditarProduto(ProdutoViewModel produto)
        {
            var response = _appProduto.EditarProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content(response.ContentAsString);
            return Content("Produto editado com sucesso!");
        }

        [HttpPut]
        public ActionResult DesativarProdutoConfirmado(ProdutoViewModel produto)
        {
            var response = _appProduto.DesativarProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            return Content("Produto desativado com sucesso!");
        }
        #endregion
    }
}