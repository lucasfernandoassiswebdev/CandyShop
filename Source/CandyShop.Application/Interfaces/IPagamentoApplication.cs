using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IPagamentoApplication
    {
        Response<IEnumerable<Pagamento>> ListarPagamentos();
        Response<IEnumerable<Pagamento>> ListarPagamentosCpf(string cpf);
        Response<string> InserirPagamento(Pagamento pagamento);
        Response<Pagamento> DetalharPagamento(int idPagamento);

    }
}
