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
            if (!produto.IsValid(_notification))
                return;
            _produtoRepository.InserirProduto(produto);
        }

        public void EditarProduto(ProdutoDto produto)
        {
            if (!produto.IsValid(_notification))
                return;

            _produtoRepository.UpdateProduto(produto);
        }
    }
}