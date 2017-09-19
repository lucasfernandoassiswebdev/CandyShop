using CandyShop.Core.Services.CompraProduto.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.CompraProduto
{
    public interface ICompraProdutoRepository
    {
        void InserirCompraProduto(CompraProdutoDto compraProduto);
        void EditarCompraProduto(CompraProdutoDto compraProduto);
        IEnumerable<CompraProdutoDto> ListarCompraProduto();
        IEnumerable<CompraProdutoDto> ListarCompraProdutoIdVenda(int idVenda);
    }
}
