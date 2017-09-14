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

       public IHttpActionResult PostProduto(ProdutoDto produto)
        {
            _produtoService.InserirProduto(produto);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());
            return Ok();
        }

       public IHttpActionResult GetListaProduto(ProdutoDto produto)
        {
            return Ok(_produtoRepository.ListarProdutos());
        }

        public IHttpActionResult PutEditaProduto(ProdutoDto produto)
        {
            _produtoRepository.UpdateProduto(produto);
            return Ok();
        }

        public IHttpActionResult DeleteProduto(int idproduto)
        {
            _produtoRepository.DeletarProduto(idproduto);
            return Ok();
        }
    }
}