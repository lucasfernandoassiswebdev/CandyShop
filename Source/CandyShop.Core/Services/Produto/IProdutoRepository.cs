using System.Collections.Generic;

namespace CandyShop.Core.Services.Produto
{
    public interface IProdutoRepository
    {
        int InserirProduto(Produto nomeProduto, out int sequencial);
        void DesativarProduto(int idProduto);
        void UpdateProduto(Produto produto);
        bool SelecionarProduto(string nomeProduto);
        IEnumerable<Produto> ListarProdutos();
        Produto SelecionarDadosProduto(int idProduto);
        IEnumerable<Produto> ListarProdutosInativos();
        IEnumerable<Produto> ListarProdutosValorCrescente();
        IEnumerable<Produto> ListarProdutosValorDecrescente();
        IEnumerable<Produto> ListarProdutosAbaixoValor();
        IEnumerable<Produto> ListarProdutosAcimaValor();
        IEnumerable<Produto> ListarProdutosPorCategoria(string categoria);
        IEnumerable<Produto> ProcurarProdutoPorNome(string nome);
    }
}

