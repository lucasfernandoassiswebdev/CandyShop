using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IProdutoApplication
    {
        Response<IEnumerable<ProdutoViewModel>> ListarProdutos();
        Response<string> InserirProduto(ProdutoViewModel produto, string token);
        Response<ProdutoViewModel> DetalharProduto(int idProduto);
        Response<string> EditarProduto(ProdutoViewModel produto, string token);
        Response<string> DesativarProduto(ProdutoViewModel produto, string token);
        Response<IEnumerable<ProdutoViewModel>> ListarInativos(string token);
        Response<IEnumerable<ProdutoViewModel>> ProcurarProduto(string nome);
        Response<IEnumerable<ProdutoViewModel>> ListarCategoria(string categoria);
    }
}
