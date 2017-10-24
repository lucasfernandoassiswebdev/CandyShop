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

        public ProdutoController(INotification notification, IProdutoRepository produtoRepository, IProdutoService produtoService) : base(produtoRepository)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
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
                    produto.ImagemA.InserirImagem($"{caminho}_A");
                else $"{caminho}_A".InserirPadrao();

                if (produto.ImagemB != null)
                    produto.ImagemB.InserirImagem($"{caminho}_B");
                else $"{caminho}_B".InserirPadrao();

                if (produto.ImagemC != null)
                    produto.ImagemC.InserirImagem($"{caminho}_C");
                else $"{caminho}_C".InserirPadrao();
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
                produto.ImagemA?.InserirImagem($"{caminho}_A");
                produto.ImagemB?.InserirImagem($"{caminho}_B");
                produto.ImagemC?.InserirImagem($"{caminho}_C");


                if (produto.RemoverImagemA)
                    $"{caminho}_A".RemoverImagem();

                if (produto.RemoverImagemB)
                    $"{caminho}_B".RemoverImagem();

                if (produto.RemoverImagemC)
                    $"{caminho}_C".RemoverImagem();
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
