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

        [HttpPost]
        public IHttpActionResult PostCompra(CompraDto compra)
        {
            int sequencial;
            var result = _compraRepository.InserirCompra(compra, out sequencial);
            if (result == -1)
                return BadRequest("Falha ao inserir compra");
            return Ok(sequencial);
        }

        [HttpPut]
        public IHttpActionResult PutCompra(CompraDto compra)
        {
            _compraRepository.EditarCompra(compra);
            return Ok();
        }

        #region Gets
        [HttpGet]
        public IHttpActionResult GetCompra()
        {
            return Ok(_compraRepository.ListarCompra());
        }

        [HttpGet, Route("api/compra/listaCompracpf/{cpf}")]
        public IHttpActionResult GetCpf(string cpf)
        {
            return Ok(_compraRepository.ListarCompraPorCpf(cpf));
        }

        [HttpGet, Route("api/compra/semana")]
        public IHttpActionResult GetSemana()
        {
            return Ok(_compraRepository.ListarCompraSemana());
        }

        [HttpGet, Route("api/compra/mes/{mes}")]
        public IHttpActionResult GetMes(int mes)
        {
            return Ok(_compraRepository.ListarCompraMes(mes));
        }
        [HttpGet, Route("api/compra/dia")]
        public IHttpActionResult GetDia()
        {
            return Ok(_compraRepository.ListarCompraDia());
        }

        [HttpGet, Route("api/compra/{nomeUsuario}")]
        public IHttpActionResult GetNome(string nomeUsuario)
        {
            return Ok(_compraRepository.ListarCompraPorNome(nomeUsuario));
        }
        #endregion
    }
}