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

        // Método Post que ira inserir uma compra e retornar um 200 (Ok)
        public IHttpActionResult PostCompra(CompraDto compra)
        {
            _compraRepository.InserirCompra(compra);
            return Ok();
        }

        //Método get que lista todas as compras e retorna a lista e um status 200 (OK)
        public IHttpActionResult GetCompra()
        {
            return Ok(_compraRepository.ListarCompra());
        }

        //Método put que edita as compras 
        public IHttpActionResult PutCompra(CompraDto compra)
        {
            _compraRepository.EditarCompra(compra);
            return Ok();
        }

        // Esse route sempre tem que ser definido quando existe métodos iguais na api pra nao gerar erro
        [Route("api/compra/listacomnpracpf/{cpf}")]
        public IHttpActionResult Get(string cpf)
        {
            return Ok(_compraRepository.ListarCompraPorCpf(cpf));
        }

        [Route("api/compra/listacomprapornome/{nomeUsuario}")]
        public IHttpActionResult GetNome(string nomeusuUsuario)
        {
            return Ok(_compraRepository.ListarCompraPorNome(nomeusuUsuario));
        }
    }
}