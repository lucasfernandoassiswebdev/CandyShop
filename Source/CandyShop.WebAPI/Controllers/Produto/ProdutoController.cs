using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers.Produto
{
    [Authorize]
    public class ProdutoController : ProdutoUnauthorizedController
    {
        private readonly string _enderecoImagens = $"{ImagensConfig.EnderecoImagens}\\Produtos";

        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly Imagens _imagens;
        public ProdutoController(INotification notification, IProdutoRepository produtoRepository, IProdutoService produtoService, Imagens imagens) : base(produtoRepository)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _imagens = imagens;
        }

        public IHttpActionResult Post(Core.Services.Produto.Produto produto)
        {
            if (produto.NomeProduto == null)
                return Content(HttpStatusCode.BadRequest, "Todas as informações do formulário devem ser preenchidas!");

            //Verificando se o produto é válido antes de inserir 
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            int sequencial;
            var result = _produtoRepository.InserirProduto(produto, out sequencial);
            if (result == -1)
                return Content(HttpStatusCode.BadRequest, "Falha ao inserir o produto");
            var caminho = $"{_enderecoImagens}\\{sequencial}";

            //salvando todas as imagens que o usuário inseriu
            try
            {
                if (produto.ImagemA != null)
                    _imagens.InserirImagem(produto.ImagemA, $"{caminho}_A");
                else _imagens.InserirPadrao($"{caminho}_A");

                if (produto.ImagemB != null)
                    _imagens.InserirImagem(produto.ImagemB, $"{caminho}_B");
                else _imagens.InserirPadrao($"{caminho}_B");

                if (produto.ImagemC != null)
                    _imagens.InserirImagem(produto.ImagemC, $"{caminho}_C");
                else _imagens.InserirPadrao($"{caminho}_C");
            }
            catch
            {
                return Content(HttpStatusCode.OK, "Produto inserido, porem ocorreu um erro ao inserir imagens");
            }
            return Content(HttpStatusCode.OK, "Produto inserido com sucesso");
        }

        [HttpPut]
        public IHttpActionResult Put(Core.Services.Produto.Produto produto)
        {
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _produtoRepository.UpdateProduto(produto);

            var caminho = $"{_enderecoImagens}\\{produto.IdProduto}";
            try
            {
                if (produto.ImagemA != null)
                    _imagens.InserirImagem(produto.ImagemA, $"{caminho}_A");
                if (produto.ImagemB != null)
                    _imagens.InserirImagem(produto.ImagemB, $"{caminho}_B");
                if (produto.ImagemC != null)
                    _imagens.InserirImagem(produto.ImagemC, $"{caminho}_C");

                if (produto.RemoverImagemA)
                    _imagens.RemoverImagem($"{caminho}_A");
                if (produto.RemoverImagemB)
                    _imagens.RemoverImagem($"{caminho}_B");
                if (produto.RemoverImagemC)
                    _imagens.RemoverImagem($"{caminho}_C");
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

        [HttpGet, Route("api/produto/inativos")]
        public IHttpActionResult GetInativos()
        {
            return Ok(_produtoRepository.ListarProdutosInativos());
        }
    }
}
