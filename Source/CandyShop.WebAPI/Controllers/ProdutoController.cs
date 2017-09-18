using CandyShop.Core.Services._Interfaces;
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
            _produtoService.InserirProduto(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok(_produtoRepository.ListarProdutos());
        }

        [HttpGet]
        [Route ("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {            
            return Ok(_produtoRepository.SelecionarDadosProduto(idProduto));
        }       

        public IHttpActionResult Put(ProdutoDto produto)
        {
            _produtoRepository.UpdateProduto(produto);
            return Ok();
        }

        public IHttpActionResult Delete(int idproduto)
        {
            //_produtoRepository.DeletarProduto(idproduto);
            return Ok();
        }
    }
}