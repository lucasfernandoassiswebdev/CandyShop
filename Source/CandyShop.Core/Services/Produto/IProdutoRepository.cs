using CandyShop.Core.Services.Produto.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Produto
{
    public interface IProdutoRepository
    {
        void InserirProduto(ProdutoDto NomeProduto);
        void DesativarProduto(int idProduto);
        void UpdateProduto(ProdutoDto produto);
        bool SelecionarProduto(string NomeProduto);
        IEnumerable<ProdutoDto> ListarProdutos();
        ProdutoDto SelecionarDadosProduto(int idProduto);
        IEnumerable<ProdutoDto> ListarProdutosInativos();
        IEnumerable<ProdutoDto> ListarProdutosValorCrescente();
        IEnumerable<ProdutoDto> ListarProdutosValorDecrescente();
        IEnumerable<ProdutoDto> ListarProdutosAbaixoValor();
        IEnumerable<ProdutoDto> ListarProdutosAcimaValor();
        IEnumerable<ProdutoDto> ListarProdutosPorCategoria();
    }
}

