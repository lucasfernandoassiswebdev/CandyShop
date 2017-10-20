using System.Collections.Generic;

namespace CandyShop.Core.Services.CompraProduto
{
    public interface ICompraProdutoRepository
    {
        void InserirCompraProduto(CompraProduto compraProduto);
        IEnumerable<CompraProduto> ListarCompraProduto();
        IEnumerable<CompraProduto> ListarCompraProdutoIdVenda(int idVenda);
    }
}
