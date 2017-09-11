using CandyShop.Core.Pagamento.Dto;
using System.Collections.Generic;

namespace CandyShop.Core
{
    public interface IPagamentoRepository
    {
        void InserirPagamento(PagamentoDto pagamento);
        void EditarPagamento(PagamentoDto pagamento);
        void DeletarPagamento(int idPagamento);
        IEnumerable<PagamentoDto> ListarPagamentos();
        IEnumerable<PagamentoDto> ListarPagamentosPorCpf(string cpf);
        bool SelecionarPagamento(int idPagamento);
        PagamentoDto SelecionarDadosPagamento(int idPagamento);

    }
}
