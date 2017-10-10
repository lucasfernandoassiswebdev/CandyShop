namespace CandyShop.Core.Services.CompraProduto
{
    public class CompraProduto
    {
        public int IdCompra { get; set; }
        public int QtdeCompra { get; set; }
        public Produto.Produto Produto { get; set; }
    }
}
