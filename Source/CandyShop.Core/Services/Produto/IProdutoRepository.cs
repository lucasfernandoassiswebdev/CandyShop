using CandyShop.Core.Services.Produto.Dto;
using System.Collections.Generic;

public interface IProdutoRepository
{
    void InserirProduto(ProdutoDto NomeProduto);
    void DeletarProduto(int idProduto);
    void UpdateProduto(ProdutoDto produto);
    bool SelecionarProduto(string NomeProduto);
    IEnumerable<ProdutoDto> ListarProdutos();
    ProdutoDto SelecionarDadosProduto(int idProduto);
}

