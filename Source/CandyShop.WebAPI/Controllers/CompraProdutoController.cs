using CandyShop.Core.CompraProduto.Dto;
using CandyShop.Core.Services.CompraProduto;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraProdutoController : ApiController
    {
        private readonly ICompraProdutoRepository _compraProdutoRepository;

        public CompraProdutoController(ICompraProdutoRepository compraProdutoRepository)
        {
            _compraProdutoRepository = compraProdutoRepository;
        }

        public IHttpActionResult PostCompraProduto(CompraProdutoDto compraProduto)
        {
            _compraProdutoRepository.InserirCompraProduto(compraProduto);
            return Ok();
        }

        public IHttpActionResult GetListaCompraProduto()
        {
            return Ok(_compraProdutoRepository.ListarCompraProduto());
        }

        public IHttpActionResult GetListaCompraProdutoPorIdCompra(int idCompra)
        {
            return Ok();
        }
    }
}