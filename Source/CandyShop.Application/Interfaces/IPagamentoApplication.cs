using CandyShop.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IPagamentoApplication
    {
        Response<string> InserirPagamento(PagamentoViewModel pagamento);
        Response<string> EditarPagamento(PagamentoViewModel pagamento);
        Response<PagamentoViewModel> SelecionarPagamento(int idPagamento);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos();
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string cpf);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(int mes);
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana();
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string cpf);        
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia();
        Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(DateTime dia);
        
    }
}
