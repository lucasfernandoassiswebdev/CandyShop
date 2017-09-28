using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;


        public ProdutoController(INotification notification, IProdutoRepository produtoRepository, IProdutoService produtoService)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        public IHttpActionResult Post(ProdutoDto produto)
        {
            _produtoService.IsValid(produto);
            if (_notification.HasNotification())            
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());            

            int sequencial;
            var result = _produtoRepository.InserirProduto(produto, out sequencial);
            if (result == -1)
                return BadRequest("Falha ao inserir o produto");
            return Ok(sequencial);
        }

        public IHttpActionResult Get()
        {
            return Ok(_produtoRepository.ListarProdutos());
        }

        [Route("api/produto/inativos")]
        public IHttpActionResult GetInativos()
        {
            return Ok(_produtoRepository.ListarProdutosInativos());
        }

        [HttpGet]
        [Route("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            return Ok(_produtoRepository.SelecionarDadosProduto(idProduto));
        }

        [HttpGet]
        [Route("api/produto/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            return Ok(_produtoRepository.ProcurarProdutoPorNome(nome));
        }

        public IHttpActionResult Put(ProdutoDto produto)
        {
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
    }
}