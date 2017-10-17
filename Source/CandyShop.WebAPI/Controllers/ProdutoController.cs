using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Hosting;
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
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            int sequencial;
            var result = _produtoRepository.InserirProduto(produto, out sequencial);
            if (result == -1)
                return Content(HttpStatusCode.BadRequest, "Falha ao inserir o produto");

            //salvando todas as imagens que o usuário inseriu
            var cont = 0;
            string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
            if (produto.ImagemA != null)
            {
                
                foreach (var prefixo in prefixos)
                {
                    if (!produto.ImagemA.StartsWith(prefixo)) continue;
                    produto.ImagemA = produto.ImagemA.Substring(prefixo.Length);

                    var bytes = Convert.FromBase64String(produto.ImagemA);                        
                    var caminho = $"{_enderecoImagens}/{sequencial}_A.jpg";
                    File.WriteAllBytes(caminho, bytes);
                    cont++;
                }
            }

            if (produto.ImagemB != null)
            {
                foreach (var prefixo in prefixos)
                {
                    if (!produto.ImagemB.StartsWith(prefixo)) continue;
                    produto.ImagemB = produto.ImagemB.Substring(prefixo.Length);

                    var bytes = Convert.FromBase64String(produto.ImagemB);
                    var caminho = $"{_enderecoImagens}/{sequencial}_B.jpg";
                    File.WriteAllBytes(caminho, bytes);
                    cont++;
                }
            }

            if (produto.ImagemC != null)
            {
                foreach (var prefixo in prefixos)
                {
                    if (!produto.ImagemC.StartsWith(prefixo)) continue;
                    produto.ImagemC = produto.ImagemC.Substring(prefixo.Length);

                    var bytes = Convert.FromBase64String(produto.ImagemC);
                    var caminho = $"{_enderecoImagens}/{sequencial}_C.jpg";
                    File.WriteAllBytes(caminho, bytes);
                    cont++;
                }
            }

            if (cont != 0 && produto.ImagemA == null) return Ok();
            {
                //pegando a imagem na aplicação e transformando em base 64
                var imagem = ConvertTo64();
                //transformando em array de bytes e salvando com o cpf do usuário
                var bytes = Convert.FromBase64String(imagem);
                var caminho = $"{_enderecoImagens}/{sequencial}_A.jpg";
                File.WriteAllBytes(caminho, bytes);
            }            
            return Ok();
        }

        public IHttpActionResult Put(Produto produto)
        {
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _produtoRepository.UpdateProduto(produto);
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
            return Ok(_produtoRepository.ListarProdutos());
        }

        [HttpGet, Route("api/produto/inativos")]
        public IHttpActionResult GetInativos()
        {
            return Ok(_produtoRepository.ListarProdutosInativos());
        }

        [HttpGet, Route("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            return Ok(_produtoRepository.SelecionarDadosProduto(idProduto));
        }

        [HttpGet, Route("api/produto/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            return Ok(_produtoRepository.ProcurarProdutoPorNome(nome));
        }

        [HttpGet, Route("api/produto/categoria/{categoria}")]
        public IHttpActionResult GetCategoria(string categoria)
        {
            return Ok(_produtoRepository.ListarProdutosPorCategoria(categoria));
        }
        #endregion

        private string ConvertTo64()
        {
            using (var image = Image.FromFile(HostingEnvironment.MapPath($"{_enderecoImagens}/sem-foto.png")))
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