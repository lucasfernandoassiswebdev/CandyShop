using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly string _enderecoImagens = $"{ImagensConfig.EnderecoImagens}\\Produtos";
        private readonly string _getEnderecoImagens = $"{ImagensConfig.GetEnderecoImagens}/Produtos";
        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;

        public ProdutoController(INotification notification, IProdutoRepository produtoRepository, IProdutoService produtoService)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        public IHttpActionResult Post(Produto produto)
        {
            if (produto.NomeProduto == null)
                return Content(HttpStatusCode.BadRequest,"Todas as informações do formulário devem ser preenchidas!");

            //Verificando se o produto é válido antes de inserir 
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            int sequencial;
            var result = _produtoRepository.InserirProduto(produto, out sequencial);
            if (result == -1)
                return Content(HttpStatusCode.BadRequest, "Falha ao inserir o produto");

            //salvando todas as imagens que o usuário inseriu
            string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
            try
            {
                if (produto.ImagemA != null)
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemA.StartsWith(prefixo)) continue;
                        produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                        var bytes = Convert.FromBase64String(produto.ImagemA);
                        var caminho = $"{_enderecoImagens}/{sequencial}_A.jpg";
                        File.WriteAllBytes(caminho, bytes);
                    }
                else InserirPadrao($"{sequencial}_A");

                if (produto.ImagemB != null)
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemB.StartsWith(prefixo)) continue;
                        produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                        var bytes = Convert.FromBase64String(produto.ImagemB);
                        var caminho = $"{_enderecoImagens}/{sequencial}_B.jpg";
                        File.WriteAllBytes(caminho, bytes);
                    }
                else InserirPadrao($"{sequencial}_B");

                if (produto.ImagemC != null)
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemC.StartsWith(prefixo)) continue;
                        produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                        var bytes = Convert.FromBase64String(produto.ImagemC);
                        var caminho = $"{_enderecoImagens}/{sequencial}_C.jpg";
                        File.WriteAllBytes(caminho, bytes);
                    }
                else InserirPadrao($"{sequencial}_C");
            }
            catch
            {
                return Content(HttpStatusCode.OK, "Erro ao inserir imagens");
            }
            return Content(HttpStatusCode.OK,"Produto inserido com sucesso");
        }

        public IHttpActionResult Put(Produto produto)
        {
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _produtoRepository.UpdateProduto(produto);
            string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
            try
            {
                if (produto.ImagemA != null)
                {
                    foreach (var prefixo in prefixos)
                    {
                        if (!produto.ImagemA.StartsWith(prefixo)) continue;
                        produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                        var bytes = Convert.FromBase64String(produto.ImagemA);
                        var caminho = $"{_enderecoImagens}\\{produto.IdProduto}_A.jpg";
                        if (File.Exists(caminho))
                            File.Delete(caminho);
                        File.WriteAllBytes(caminho, bytes);
                    }
                }                

                if (produto.ImagemB != null)
                {                
                    foreach (var prefixo in prefixos)
                        if (produto.ImagemB.StartsWith(prefixo))
                        {
                            produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                            var bytes = Convert.FromBase64String(produto.ImagemB);
                            var caminho = $"{_enderecoImagens}\\{produto.IdProduto}_B.jpg";
                            if (File.Exists(caminho))
                                File.Delete(caminho);
                            File.WriteAllBytes(caminho, bytes);
                        }
                }

                if (produto.ImagemC != null)
                {
                    foreach (var prefixo in prefixos)
                        if (produto.ImagemC.StartsWith(prefixo))
                        {
                            produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                            var bytes = Convert.FromBase64String(produto.ImagemC);
                            var caminho = $"{_enderecoImagens}\\{produto.IdProduto}_C.jpg";
                            if (File.Exists(caminho))
                                File.Delete(caminho);
                            File.WriteAllBytes(caminho, bytes);
                        }
                }

                if (produto.RemoverImagemA)
                {
                    var filePath = $"{_enderecoImagens}\\{produto.IdProduto}_A.jpg";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        InserirPadrao($"{produto.IdProduto}_A");
                    }
                }

                if (produto.RemoverImagemB)
                {
                    var filePath = $"{_enderecoImagens}\\{produto.IdProduto}_B.jpg";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        InserirPadrao($"{produto.IdProduto}_B");
                    }
                }
                
                if (produto.RemoverImagemC)
                {
                    var filePath = $"{_enderecoImagens}\\{produto.IdProduto}_C.jpg";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        InserirPadrao($"{produto.IdProduto}_C");
                    }
                }
            }
            catch
            {
                return Content(HttpStatusCode.NotModified, "Erro ao editar imagens");
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/produto/desativar/{idProduto}")]
        public IHttpActionResult PutDesativar(int idproduto)
        {
            _produtoRepository.DesativarProduto(idproduto);
            return Ok();
        }

        #region GETS
        public IHttpActionResult Get()
        {
            var produtos = _produtoRepository.ListarProdutos();
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }

        [HttpGet, Route("api/produto/inativos")]
        public IHttpActionResult GetInativos()
        {
            return Ok(_produtoRepository.ListarProdutosInativos());
        }

        [HttpGet, Route("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            var produto = _produtoRepository.SelecionarDadosProduto(idProduto);
            produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
            produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
            produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            return Ok(produto);
        }

        [HttpGet, Route("api/produto/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            var produtos = _produtoRepository.ProcurarProdutoPorNome(nome);
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }

        [HttpGet, Route("api/produto/categoria/{categoria}")]
        public IHttpActionResult GetCategoria(string categoria)
        {
            var produtos = _produtoRepository.ListarProdutosPorCategoria(categoria);
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }
        #endregion

        private void InserirPadrao(string nome)
        {
            //pegando a imagem na aplicação e transformando em base 64
            var imagem = ConvertTo64();
            //transformando em array de bytes e salvando com o cpf do usuário
            var bytes = Convert.FromBase64String(imagem);
            var caminho = $"{_enderecoImagens}/{nome}.jpg";
            File.WriteAllBytes(caminho, bytes);
        }

        private string ConvertTo64()
        {
            using (var image = Image.FromFile($"{_enderecoImagens}/sem-foto.png"))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    var base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}