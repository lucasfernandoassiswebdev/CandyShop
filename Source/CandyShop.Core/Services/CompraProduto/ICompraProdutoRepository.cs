using CandyShop.Core.CompraProduto.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.CompraProduto
{
    public interface ICompraProdutoRepository
    {
        void InserirCompraProduto(CompraProdutoDto compra);
        void EditarCompraProduto(CompraProdutoDto compra);
        IEnumerable<CompraProdutoDto> ListarCompraProduto();
    }
}
