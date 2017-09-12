using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Core.Services.Produto
{
    public interface IProdutoService
    {
        void InserirProduto(ProdutoDto produto);
    }
}