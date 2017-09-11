using CandyShop.Core.Services.Produto.Dto;
using System.Collections.Generic;

public interface IProdutoRepository
{
    void InserirProduto(ProdutoDto produto);
    void DeletarProduto(int idProduto);
    void UpdateProduto(ProdutoDto produto);
    bool SelecionarProduto(int idProduto);
    IEnumerable<ProdutoDto> ListarProdutos();
    ProdutoDto SelecionarDadosProduto(int idProduto);
}

