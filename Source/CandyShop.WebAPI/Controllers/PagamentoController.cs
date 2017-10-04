using CandyShop.Core.Services;
using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Pagamento.Dto;
using System;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
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

        public IHttpActionResult Post(PagamentoDto pagamento)
        {
            try
            {
                _pagamentoService.ValidarPagamento(pagamento);
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                _pagamentoRepository.InserirPagamento(pagamento);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }


        public IHttpActionResult Put(PagamentoDto pagamento)
        {
            try
            {
                _pagamentoService.ValidarPagamento(pagamento);
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                _pagamentoRepository.EditarPagamento(pagamento);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }

        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentos());

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        [Route("api/pagamento/id/{idPagamento}")]
        public IHttpActionResult GetEspecifico(int idPagamento)
        {
            try
            {
                return Ok(_pagamentoRepository.SelecionarDadosPagamento(idPagamento));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        [Route("api/pagamento/{cpf}")]
        public IHttpActionResult GetCpf(string cpf)
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentos(cpf));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
            
        }

        [Route("api/pagamento/mes/{mes}")]
        public IHttpActionResult GetMes(int mes)
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentos(mes));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        [Route("api/pagamento/semana")]
        public IHttpActionResult GetSemana()
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentoSemana());

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);

            }
        }

        [Route("api/pagamento/semana/{cpf}")]
        public IHttpActionResult GetSemanaCpf(string cpf)
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentoSemana(cpf));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);

            }
        }

        [Route("api/pagamento/dia")]
        public IHttpActionResult GetDia()
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentoDia());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);

            }
        }

        [Route("api/pagamento/dia/{dia}")]
        public IHttpActionResult GetDia(DateTime dia)
        {
            try
            {
                return Ok(_pagamentoRepository.ListarPagamentoDia(dia));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        public IHttpActionResult Delete(int idpagamento)
        {
            try
            {
                _pagamentoRepository.DeletarPagamento(idpagamento);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
            
        }
    }
}