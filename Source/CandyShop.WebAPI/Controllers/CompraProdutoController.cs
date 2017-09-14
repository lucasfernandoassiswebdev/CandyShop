using System.Web.Http;
using CandyShop.Core.Services.CompraProduto;
using System.Web.Mvc;
using CandyShop.Core.CompraProduto.Dto;

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