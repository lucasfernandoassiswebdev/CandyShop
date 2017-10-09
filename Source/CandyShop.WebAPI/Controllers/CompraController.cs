using CandyShop.Core.Services;
using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraController : ApiController
    {
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        private readonly INotification _notification;
        private readonly CompraService _appService;

        public CompraController(ICompraRepository compraRepository, ICompraProdutoRepository compraProdutoRepository, INotification notification, CompraService service)
        {
            _compraRepository = compraRepository;
            _compraProdutoRepository = compraProdutoRepository;
            _notification = notification;
            _appService = service;
        }

        //Método post para inserir uma compra, se der erro adiciona uma notification senão retorna Ok(200)
        [HttpPost]
        public IHttpActionResult PostCompra(CompraDto compra)
        {
            var result = _appService.InserirCompra(compra);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            return Ok(result);
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

        [HttpGet, Route("api/compra/selecionarcompra/{idCompra}")]
        public IHttpActionResult GetUmaCompra(int idCompra)
        {
            var compra = _compraRepository.SelecionarDadosCompra(idCompra);
            compra.Itens = _compraProdutoRepository.ListarCompraProdutoIdVenda(idCompra);
            return Ok(compra);
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