using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface ICompraInterface
    {
        Response<string> InserirCompra(CompraViewModel compra);
        Response<string> EditarCompra(CompraViewModel compra);
        Response<string> InserirItens(ProdutoViewModel produto);
        Response<IEnumerable<CompraViewModel>> ListaCompra();
        Response<IEnumerable<CompraViewModel>> ListaCompraPorCpf(string cpf);
        Response<CompraViewModel> SelecionarCompra(int idcompra);        
        Response<IEnumerable<CompraViewModel>> ListaCompraPorNome(string nomeUsuario);
    }
}