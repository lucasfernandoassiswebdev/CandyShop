using CandyShop.Core;
using CandyShop.Core.Compra.Dto;
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

        public IHttpActionResult PutCompra()
        {
            
        }
    }
}