namespace CandyShop.Application.ViewModels
{
    /* Essas classes, assim como o nome indica servem além de representações
       das entidades, para que sejam construídas e retornados os objetos das 
       views e usados nos controllers */
    public class CompraProdutoViewModel
    {
        public int IdCompra { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public int QtdeCompra { get; set; }
    }
}
