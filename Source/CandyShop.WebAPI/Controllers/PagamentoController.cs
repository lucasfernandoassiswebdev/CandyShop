using CandyShop.Core;
using CandyShop.Core.Pagamento.Dto;
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

        public IHttpActionResult PostPagamento(PagamentoDto pagamento)
        {
            _pagamentoRepository.InserirPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult PutPagamento(PagamentoDto pagamento)
        {
            _pagamentoRepository.EditarPagamento(pagamento);
            return Ok();
        }

        public IHttpActionResult GetPagamento(PagamentoDto pagamento )
        {
            _pagamentoRepository.ListarPagamentos();
            return Ok();
        }

        public IHttpActionResult DeletePagamento(int idpagamento)
        {
            _pagamentoRepository.DeletarPagamento(idpagamento);
            return Ok();
        }
    }
}