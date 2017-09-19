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

        public IHttpActionResult Post(CompraProdutoDto compraProduto)
        {
            _compraProdutoRepository.InserirCompraProduto(compraProduto);
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok(_compraProdutoRepository.ListarCompraProduto());
        }

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