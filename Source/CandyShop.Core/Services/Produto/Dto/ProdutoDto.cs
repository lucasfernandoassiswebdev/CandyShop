namespace CandyShop.Core.Services.Produto.Dto
{
    public class ProdutoDto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QtdeProduto { get; set; }
        public string Ativo { get; set; }
        public string Categoria { get; set; }



        public bool IsValid(INotification notification)
        {
            if (string.IsNullOrEmpty(NomeProduto.Trim()) || NomeProduto.Length > 40)
                notification.Add("Nome do produto invalido");

            if (PrecoProduto <= 0)
                notification.Add("Preço do produto nao pode ser negativo ou zerado");
            
            if (string.IsNullOrEmpty(Ativo))
                notification.Add("Status do produto nao pode ser nulo");

            return !notification.HasNotification();
        }
    }
}


