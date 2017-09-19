using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Compra
{
    public interface ICompraRepository
    {
        void InserirCompra(CompraDto compra);
        void EditarCompra(CompraDto compra);
        void DeletarCompra(int idCompra);
        IEnumerable<CompraDto> ListarCompra();
        IEnumerable<CompraDto> ListarCompraPorCpf(string cpf);
        int SelecionarCompra(int idCompra);
        CompraDto SelecionarDadosCompra(int idCompra);
        void InserirItens(CompraProdutoDto compraProduto);
        void EditaItens(CompraProdutoDto compraProduto);
        void DeletaItens(int idcompra, int idproduto);
        IEnumerable<CompraDto> ListarCompraPorNome(string nome);
    }
}
