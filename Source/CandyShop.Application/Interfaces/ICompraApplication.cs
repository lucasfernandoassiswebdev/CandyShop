using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface ICompraApplication
    {
        Response<string> InserirCompra(CompraViewModel compra);
        Response<string> EditarCompra(CompraViewModel compra);
        Response<string> InserirItens(ProdutoViewModel produto);
        Response<CompraViewModel> SelecionarCompra(int idcompra);
        Response<IEnumerable<CompraViewModel>> ListaCompra();
        Response<IEnumerable<CompraViewModel>> ListaCompraPorCpf(string cpf);                
        Response<IEnumerable<CompraViewModel>> ListaCompraPorNome(string nomeUsuario);
        Response<IEnumerable<CompraViewModel>> ListarComprasSemana();
        Response<IEnumerable<CompraViewModel>> ListarComprasMes(int mes);
        Response<IEnumerable<CompraViewModel>> ListarComprasDia();
    }
}