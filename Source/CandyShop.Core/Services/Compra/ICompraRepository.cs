﻿using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Compra
{
    public interface ICompraRepository
    {
        int InserirCompra(CompraDto compra, out int sequencial);
        void EditarCompra(CompraDto compra);
        void DeletarCompra(int idCompra);        
        int SelecionarCompra(int idCompra);
        CompraDto SelecionarDadosCompra(int idCompra);        
        void EditaItens(CompraProdutoDto compraProduto);
        void DeletaItens(int idcompra, int idproduto);
        IEnumerable<CompraDto> ListarCompra();
        IEnumerable<CompraDto> ListarCompraPorNome(string nome);        
        IEnumerable<CompraDto> ListarCompraPorCpf(string cpf);
        IEnumerable<CompraDto> ListarCompraSemana();
        IEnumerable<CompraDto> ListarCompraMes(int mes);
        IEnumerable<CompraDto> ListarCompraDia();
        void CommitTransaction();
        void RollBackTransaction();
        void BeginTransaction();
    }
}
