using CandyShop.Core.Services;
using CandyShop.Core.Services.Pagamento;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    [Authorize]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly INotification _notification;
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(IPagamentoRepository pagamentoRepository, INotification notification, IPagamentoService pagamentoService)
        {
            _pagamentoRepository = pagamentoRepository;
            _notification = notification;
            _pagamentoService = pagamentoService;
        }

        public IHttpActionResult Post(Pagamento pagamento)
        {
            _pagamentoService.ValidarPagamento(pagamento);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _pagamentoRepository.InserirPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult Put(Pagamento pagamento)
        {
            _pagamentoService.ValidarPagamento(pagamento);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _pagamentoRepository.EditarPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok(_pagamentoRepository.ListarPagamentos());
        }
        [Route("api/pagamento/id/{idPagamento}")]
        public IHttpActionResult GetEspecifico(int idPagamento)
        {
            return Ok(_pagamentoRepository.SelecionarDadosPagamento(idPagamento));
        }
        [Route("api/pagamento/{cpf}")]
        public IHttpActionResult GetCpf(string cpf)
        {
            return Ok(_pagamentoRepository.ListarPagamentos(cpf));
        }
        [Route("api/pagamento/mes/{mes}")]
        public IHttpActionResult GetMes(int mes)
        {
            return Ok(_pagamentoRepository.ListarPagamentos(mes));
        }
        [Route("api/pagamento/semana")]
        public IHttpActionResult GetSemana()
        {
            return Ok(_pagamentoRepository.ListarPagamentoSemana());
        }
        [Route("api/pagamento/semana/{cpf}")]
        public IHttpActionResult GetSemanaCpf(string cpf)
        {
            return Ok(_pagamentoRepository.ListarPagamentoSemana(cpf));
        }
        [Route("api/pagamento/dia")]
        public IHttpActionResult GetDia()
        {
            return Ok(_pagamentoRepository.ListarPagamentoDia());
        }
    }
}