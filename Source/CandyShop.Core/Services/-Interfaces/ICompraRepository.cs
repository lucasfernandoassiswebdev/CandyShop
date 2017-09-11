using CandyShop.Core.Compra.Dto;
using System.Collections.Generic;

namespace CandyShop.Core
{
    public interface ICompraRepository
    {
        void InserirCompra(CompraDto compra);
        void EditarCompra(CompraDto compra);
        void DeletarCompra(int idCompra);
        IEnumerable<CompraDto> ListarCompra();
        IEnumerable<CompraDto> ListarCompraPorCpf(string cpf);
        bool SelecionarCompra(int idCompra);
        CompraDto SelecionarDadosCompra(int idCompra);
    }
}
