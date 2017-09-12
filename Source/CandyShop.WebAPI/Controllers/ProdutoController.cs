using CandyShop.Core.Services._Interfaces;
using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;


        public ProdutoController(INotification notification, IProdutoRepository produtoRepository,
            IProdutoService produtoService)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        IHttpActionResult PostProduto(ProdutoDto produto)
        {
            
        }
    }
}