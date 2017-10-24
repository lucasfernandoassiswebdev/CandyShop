using CandyShop.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IPagamentoApplication
    {
        Response<string> InserirPagamento(PagamentoViewModel pagamento,string token);
        Response<string> EditarPagamento(PagamentoViewModel pagamento,string token);
        Response<PagamentoViewModel> SelecionarPagamento(int idPagamento, string token);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string token);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string cpf,string token);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(int mes, string token);

        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string token);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string cpf, string token);

        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(string token);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(DateTime dia,string token);
    }
}
