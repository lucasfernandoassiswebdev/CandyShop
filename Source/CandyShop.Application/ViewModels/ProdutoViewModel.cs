namespace CandyShop.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QtdeProduto { get; set; }
        public string Ativo { get; set; }
        public string Categoria { get; set; }
        public string ImagemA { get; set; }
        public string ImagemB { get; set; }
        public string ImagemC { get; set; }
        public bool RemoverImagemA { get; set; }
        public bool RemoverImagemB { get; set; }
        public bool RemoverImagemC { get; set; }
    }
}