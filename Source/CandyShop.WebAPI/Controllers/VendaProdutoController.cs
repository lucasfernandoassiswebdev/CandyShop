using CandyShop.Core;
using CandyShop.Core.Compra.Dto;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class VendaProdutoController : ApiController
    {
        private readonly ICompraRepository _compraRepository;

        public VendaProdutoController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public IHttpActionResult PostCompraProduto(CompraDto compra)
        {
            _compraRepository.InserirCompra(compra);
            foreach (var item in compra.Itens)
            {
                _compraRepository.InserirItens(item);
            }
            return Ok();
        }
    }
}