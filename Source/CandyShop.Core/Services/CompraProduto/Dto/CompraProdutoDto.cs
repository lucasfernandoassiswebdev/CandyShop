using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Core.Services.CompraProduto.Dto
{
    public class CompraProdutoDto
    {
        public int IdCompra { get; set; }
        public int QtdeCompra { get; set; }
        public ProdutoDto Produto { get; set; }
    }
}
