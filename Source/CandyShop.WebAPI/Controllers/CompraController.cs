using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Compra.Dto;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraController : ApiController
    {
        private readonly ICompraRepository _compraRepository;

        public CompraController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public IHttpActionResult PostCompra(CompraDto compra)
        {
            _compraRepository.InserirCompra(compra);
            return Ok();
        }

        public IHttpActionResult GetCompra()
        {
            return Ok(_compraRepository.ListarCompra());
        }

        public IHttpActionResult PutCompra(CompraDto compra)
        {
            _compraRepository.EditarCompra(compra);
            return Ok();
        }
    }
}