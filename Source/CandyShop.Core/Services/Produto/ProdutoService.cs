using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Core.Services.Produto
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly INotification _notification;

        public ProdutoService(INotification notification, IProdutoRepository produtoRepository)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
        }        
        // Método IsValid on são feitas todas as verificaçoes de produto
        public void IsValid(ProdutoDto produto)
        {
            if (string.IsNullOrEmpty(produto.NomeProduto.Trim()) || produto.NomeProduto.Length > 40)
            {
                _notification.Add("Nome do produto invalido");
                return;
            }

            if (produto.PrecoProduto <= 0)
            {
                _notification.Add("Preço do produto nao pode ser negativo ou zerado");
                return;
            }

            if (string.IsNullOrEmpty(produto.QtdeProduto.ToString()))
            {
                _notification.Add("Quantidade do produto nao pode ser nula");
                return;
            }

            //verificando se já existe um produto com o mesmo nome
            var produtos = _produtoRepository.ListarProdutos();
            foreach (var item in produtos)
            {
                if (item.NomeProduto.Equals(produto.NomeProduto) && item.IdProduto != produto.IdProduto)
                {
                    _notification.Add("Produto já existente");
                    return;
                }                

            }
        }
    }
}