using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Pagamento.Dto;
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

        public IHttpActionResult Get(PagamentoDto pagamento )
        {
            return Ok(_pagamentoRepository.ListarPagamentos());
        }

        public IHttpActionResult Delete(int idpagamento)
        {
            _pagamentoRepository.DeletarPagamento(idpagamento);
            return Ok();
        }
    }
}