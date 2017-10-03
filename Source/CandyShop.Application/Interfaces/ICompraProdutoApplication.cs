using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface ICompraProdutoApplication
    {
        Response<IEnumerable<CompraProdutoViewModel>> ListarProdutos(int idCompra);
    }
}
