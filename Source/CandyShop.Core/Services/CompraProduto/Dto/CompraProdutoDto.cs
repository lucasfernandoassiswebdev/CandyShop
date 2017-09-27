using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Core.Services.CompraProduto.Dto
{
    public class CompraProdutoDto
    {
        private readonly IProdutoRepository _produtoRepository;

        public CompraProdutoDto(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public int IdCompra { get; set; }
        public int QtdeCompra { get; set; }
        public ProdutoDto Produto { get; set; }

        public bool IsValid(INotification notification)
        {
            if (VerificaEstoque(QtdeCompra))
                notification.Add("Quantidade da compra indisponível no estoque!");            

            return !notification.HasNotification();
        }

        private bool VerificaEstoque(int qtde)
        {            
            var estoque = _produtoRepository.SelecionarDadosProduto(Produto.IdProduto).QtdeProduto;
            return qtde > estoque;
        }

    }
}
