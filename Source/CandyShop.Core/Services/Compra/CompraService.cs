using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Produto;
using System;

namespace CandyShop.Core.Services.Compra
{

    public class CompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;

        public CompraService(ICompraRepository compraRepositorio, ICompraProdutoRepository compraProdutoRepository, INotification notification, IProdutoRepository produtoRepository)
        {
            _compraRepository = compraRepositorio;
            _compraProdutoRepository = compraProdutoRepository;
            _notification = notification;
            _produtoRepository = produtoRepository;
        }

        public int InserirCompra(CompraDto compra)
        {
            _compraRepository.BeginTransaction();
            try
            {
                int valor = 0;
                var result = _compraRepository.InserirCompra(compra, out valor);
                if (result == -1)
                {
                    _compraRepository.RollBackTransaction();
                    _notification.Add("Falha ao inserir compra");
                    return 0;
                }

                foreach (var item in compra.Itens)
                {
                    if (item.QtdeCompra <= 0)
                    {
                        _notification.Add("Quantidade do produto nao pode ser zero ou menor");
                    }

                    VerificaEstoque(item);
                    if (_notification.HasNotification())
                    {
                        _compraRepository.RollBackTransaction();
                        return 0;
                    }

                    item.IdCompra = valor;
                    _compraProdutoRepository.InserirCompraProduto(item);
                }

                _compraRepository.CommitTransaction();
                return valor;
            }
            catch (Exception e)
            {
                _compraRepository.RollBackTransaction();
                _notification.Add("Erro ao inserir compra " + e.Message);
                return 0;
            }
        }

        private void VerificaEstoque(CompraProdutoDto item)
        {
            var consulta = _produtoRepository.SelecionarDadosProduto(item.Produto.IdProduto);
            var estoque = consulta.QtdeProduto;
            if (item.QtdeCompra > estoque)
                _notification.Add($"Quantidade de {consulta.NomeProduto} indisponível no estoque!");
        }
    }
}
