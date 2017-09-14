using CandyShop.Core;
using CandyShop.Core.Compra.Dto;
using CandyShop.Core.CompraProduto.Dto;
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

        public IHttpActionResult GetListaVenda(int idcompra, int qtdecompra)
        {
            _compraRepository.ListarCompra();
            return Ok();
        }

        public IHttpActionResult PutEditaVenda(CompraProdutoDto compraproduto)
        {
            _compraRepository.EditaItens(compraproduto);
            return Ok();
        }

        public IHttpActionResult DeleteItens(int idcompra, int idproduto)
        {
            _compraRepository.DeletaItens(idcompra,idproduto);
            return Ok();
        }

    }
}