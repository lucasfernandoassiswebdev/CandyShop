using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IProdutoApplication
    {
        Response<IEnumerable<ProdutoViewModel>> ListarProdutos();
        Response<int> InserirProduto(ProdutoViewModel produto);
        Response<ProdutoViewModel> DetalharProduto(int idProduto);
        Response<string> EditarProduto(ProdutoViewModel produto);
        Response<string> DesativarProduto(ProdutoViewModel produto);
        Response<IEnumerable<ProdutoViewModel>> ListarInativos();
        Response<IEnumerable<ProdutoViewModel>> ProcurarProduto(string nome);
        Response<IEnumerable<ProdutoViewModel>> ListarCategoria(string categoria);
    }
}
