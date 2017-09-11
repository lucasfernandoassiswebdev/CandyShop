using CandyShop.Core.Compra.Dto;
using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Core.CompraProduto.Dto
{
    public class CompraProdutoDto
    {
        public CompraDto Compra { get; set; }
        public ProdutoDto Produto { get; set; }
        public int QtdeCompra { get; set; }
    }
}
