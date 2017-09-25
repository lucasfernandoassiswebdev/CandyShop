using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IProdutoApplication
    {
        Response<IEnumerable<ProdutoViewModel>> ListarProdutos();
        Response<string> InserirProduto(ProdutoViewModel produto);
        Response<ProdutoViewModel> DetalharProduto(int idProduto);
        Response<string> EditarProduto(ProdutoViewModel produto);
        Response<string> DesativarProduto(int idProduto);
        Response<IEnumerable<ProdutoViewModel>> ListarInativos();
        Response<IEnumerable<ProdutoViewModel>> ProcurarProduto(string nome);
        Response<int> BuscaUltimoProduto();
    }
}
