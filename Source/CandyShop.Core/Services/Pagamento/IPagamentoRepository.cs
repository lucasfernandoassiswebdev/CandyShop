using System.Collections.Generic;

namespace CandyShop.Core.Services.Pagamento
{
    public interface IPagamentoRepository
    {
        void InserirPagamento(Pagamento pagamento);
        void EditarPagamento(Pagamento pagamento);
        void DeletarPagamento(int idPagamento);
        IEnumerable<Pagamento> ListarPagamentos(string cpf);
        IEnumerable<Pagamento> ListarPagamentos();
        IEnumerable<Pagamento> ListarPagamentoSemana(string cpf);
        IEnumerable<Pagamento> ListarPagamentoSemana();
        bool SelecionarPagamento(int idPagamento);
        Pagamento SelecionarDadosPagamento(int idPagamento);
        IEnumerable<Pagamento> ListarPagamentoDia();
        IEnumerable<Pagamento> ListarPagamentos(int mes);
    }
}
