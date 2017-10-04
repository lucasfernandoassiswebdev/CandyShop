using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using System;
using System.Linq;
using System.Net;
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
            try
            {
                return Ok(_compraProdutoRepository.ListarCompraProduto());

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/CompraProduto/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_compraProdutoRepository.ListarCompraProdutoIdVenda(id));

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        public IHttpActionResult Put(CompraProdutoDto compraProduto)
        {
            try
            {
                _compraProdutoRepository.EditarCompraProduto(compraProduto);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }
    }
}