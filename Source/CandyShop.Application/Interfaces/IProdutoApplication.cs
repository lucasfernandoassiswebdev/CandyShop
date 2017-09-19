using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IProdutoApplication
    {
        Response<IEnumerable<Produto>> ListarProdutos();
        Response<string> InserirProduto(Produto produto);
        Response<Produto> DetalharProduto(int idProduto);
        Response<string> EditarProduto(Produto produto);
        Response<string> DesativarProduto(int idProduto);
        Response<IEnumerable<Produto>> ListarInativos();
    }
}
