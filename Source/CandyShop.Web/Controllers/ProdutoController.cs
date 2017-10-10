using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                return Content("Erro" + response.ContentAsString);
            TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }

        [AdminFilterResult]
        public ActionResult EditarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
            return View(response.Content);
        }

        [AdminFilterResult]
        public ActionResult DesativarProduto(int idProduto, string telaAnterior)
        {
            var response = _appProduto.DetalharProduto(idProduto);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
            return View(response.Content);
        }
        #endregion

        #region Listas
        [AdminFilterResult]
        public ActionResult Listar()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString);

            TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
            TempData["nomeLista"] = "Produtos Ativos";
            return View("ListaProdutos", response.Content);

        }

        [AdminFilterResult]
        public ActionResult ListarInativos()
        {
            var response = _appProduto.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro {response.ContentAsString}");

            TempData["nomeLista"] = "Produtos Inativos";

            return View("ListaProdutos", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ProcurarProduto(string nome)
        {
            var response = _appProduto.ProcurarProduto(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");

            TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
            TempData["nomeLista"] = "Produtos relacionados";
            return View("ListaProdutos", response.Content);
        }

        //[AdminFilterResult]
        //public ActionResult ListarCategoria(string categoria)
        //{
        //    var response = _appProduto.ListarCategoria(categoria);
        //    if (response.Status != HttpStatusCode.OK)
        //        return Content($"Erro: {response.Status}");

        //    TempData["nomeLista"] = "Produtos por Categoria";
        //    TempData["caminhoImagensProdutos"] = "Imagens/Produtos";
        //    return View("ListaProdutos", response.Content);
        //}

        #endregion

        #region Execucoes
        [AdminFilterResult]
        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var response = _appProduto.InserirProduto(produto);
                if (response.Status != HttpStatusCode.OK)
                    return Content(response.ContentAsString);

                //salvando todas as imagens que o usuário inseriu
                int cont = 0;
                if (produto.ImagemA != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemA.StartsWith(prefixo)) continue;
                        produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                        byte[] bytes = Convert.FromBase64String(produto.ImagemA);

                        Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                        string caminho = $"Imagens/Produtos/{response.Content}_A.jpg";

                        imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        cont++;
                    }
                }
                else
                {
                    var filePath = Server.MapPath("Imagens/Produtos/" + produto.IdProduto + "_A.jpg");
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                if (produto.ImagemB != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemB.StartsWith(prefixo)) continue;
                        produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                        byte[] bytes = Convert.FromBase64String(produto.ImagemB);

                        Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                        string caminho = $"Imagens/Produtos/{response.Content}_B.jpg";

                        imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        cont++;
                    }
                }
                else
                {
                    var filePath = Server.MapPath("Imagens/Produtos/" + produto.IdProduto + "_B.jpg");
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                }

                if (produto.ImagemC != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemC.StartsWith(prefixo)) continue;
                        produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                        byte[] bytes = Convert.FromBase64String(produto.ImagemC);

                        Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

                        string caminho = $"Imagens/Produtos/{response.Content}_C.jpg";

                        imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        cont++;
                    }
                }
                else
                {
                    var filePath = Server.MapPath("Imagens/Produtos/" + produto.IdProduto + "_C.jpg");
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                if (cont == 0)
                {
                    //pegando a imagem na aplicação e transformando em base 64
                    string imagem = ConvertTo64();
                    //transformando em array de bytes e salvando com o cpf do usuário
                    byte[] bytes = Convert.FromBase64String(imagem);

                    Image imagem2 = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

                    string caminho = $"Imagens/Produtos/{response.Content}_A.jpg";

                    imagem2.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                }

                return Content("Produto cadastrado com sucesso!!");
            }

            return Content("Ops, ocorreu um erro ao editar o produto");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult EditarProduto(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {

                var response = _appProduto.EditarProduto(produto);
                if (response.Status != HttpStatusCode.OK)
                    return Content(response.ContentAsString);

                if (produto.ImagemA != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (produto.ImagemA.StartsWith(prefixo))
                        {
                            produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                            byte[] bytes = Convert.FromBase64String(produto.ImagemA);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                            string caminho = $"Imagens/Produtos/{produto.IdProduto}_A.jpg";

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

                            byte[] bytes = Convert.FromBase64String(produto.ImagemB);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));


                            string caminho = $"Imagens/Produtos/{produto.IdProduto}_B.jpg";

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

                            byte[] bytes = Convert.FromBase64String(produto.ImagemC);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

                            string caminho = $"Imagens/Produtos/{produto.IdProduto}_C.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                return Content("Produto editado com sucesso!");
            }

            return Content("Ops, ocorreu um erro ao editar o produto");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult DesativarProdutoConfirmado(ProdutoViewModel produto)
        {
            var response = _appProduto.DesativarProduto(produto);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            return Content("Produto desativado com sucesso!");
        }
        #endregion

        private string ConvertTo64()
        {
            using (Image image = Image.FromFile(Server.MapPath("Imagens/Produtos/sem-foto.png")))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}

