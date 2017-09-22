using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Pagamento.Dto;
using System;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        

        public PagamentoController(IPagamentoRepository pagamentoRepository)
        {
           _pagamentoRepository = pagamentoRepository;
        }

        public IHttpActionResult Post(PagamentoDto pagamento)
        {
            _pagamentoRepository.InserirPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult Put(PagamentoDto pagamento)
        {
            _pagamentoRepository.EditarPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok(_pagamentoRepository.ListarPagamentos());
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

        [Route("api/pagamento/dia/{dia}")]
        public IHttpActionResult GetDia(DateTime dia)
        {
            return Ok(_pagamentoRepository.ListarPagamentoDia(dia));
        }

        public IHttpActionResult Delete(int idpagamento)
        {
            _pagamentoRepository.DeletarPagamento(idpagamento);
            return Ok();
        }
    }
}