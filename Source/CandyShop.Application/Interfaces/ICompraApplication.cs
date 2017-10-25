using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface ICompraApplication
    {
        Response<int> InserirCompra(CompraViewModel compra, string token);
        Response<CompraViewModel> SelecionarCompra(int idcompra, string token);
        Response<IEnumerable<CompraViewModel>> ListaCompra(string token);
        Response<IEnumerable<CompraViewModel>> ListaCompraPorCpf(string cpf, string token);
        Response<IEnumerable<CompraViewModel>> ListaCompraPorNome(string nomeUsuario, string token);
        Response<IEnumerable<CompraViewModel>> ListarComprasSemana(string token);
        Response<IEnumerable<CompraViewModel>> ListarComprasMes(int mes, string token);
        Response<IEnumerable<CompraViewModel>> ListarComprasDia(string token);
    }
}