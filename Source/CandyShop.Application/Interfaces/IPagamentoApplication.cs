using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IPagamentoApplication
    {
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos();
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string cpf);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana();
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string cpf);
        Response<string> InserirPagamento(PagamentoViewModel pagamento);
        Response<PagamentoViewModel> DetalharPagamento(int idPagamento);

    }
}
