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

        public void InserirProduto(ProdutoDto produto)
        {
            if (_produtoRepository.SelecionarProduto(produto.NomeProduto.Trim()))
            {
                _notification.Add("Produto ja existe !!!");
                return;
            }
            _produtoRepository.InserirProduto(produto);
        }

    }
}