using System;
using CandyShop.Core.Services.Pagamento.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Pagamento
{
    public interface IPagamentoRepository
    {
        void InserirPagamento(PagamentoDto pagamento);
        void EditarPagamento(PagamentoDto pagamento);
        void DeletarPagamento(int idPagamento);
        IEnumerable<PagamentoDto> ListarPagamentos(string cpf);
        IEnumerable<PagamentoDto> ListarPagamentos();
        IEnumerable<PagamentoDto> ListarPagamentoSemana(string cpf);
        IEnumerable<PagamentoDto> ListarPagamentoSemana();
        bool SelecionarPagamento(int idPagamento);
        PagamentoDto SelecionarDadosPagamento(int idPagamento);
        IEnumerable<PagamentoDto> ListarPagamentoDia();
        IEnumerable<PagamentoDto> ListarPagamentoDia(DateTime data);
        IEnumerable<PagamentoDto> ListarPagamentos(int mes);
    }
}
