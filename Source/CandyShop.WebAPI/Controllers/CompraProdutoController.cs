using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
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

        public IHttpActionResult Get()
        {
            return Ok(_compraProdutoRepository.ListarCompraProduto());
        }

        [HttpGet, Route("api/CompraProduto/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_compraProdutoRepository.ListarCompraProdutoIdVenda(id));
        }

        public IHttpActionResult Put(CompraProdutoDto compraProduto)
        {
            _compraProdutoRepository.EditarCompraProduto(compraProduto);
            return Ok();
        }
    }
}