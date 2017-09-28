﻿using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Produto.Dto;
using CandyShop.Repository.Database;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class CompraProdutoRepository : Execucao, ICompraProdutoRepository
    {
        public CompraProdutoRepository(Conexao conexao) : base(conexao)
        {

        }

        //Cria uma lista com as procedures do banco pra serem usadas 
        private enum Procedures
        {
            CSSP_LisCompraProduto,
            CSSP_LisCompraProdutoIdVenda,
            CSSP_InsCompraProduto,
            CSSP_UpdCompraProduto
        }

        /* Cria o metodo usando o ExecuteProcedure que é o metodo encapsulado para executar as procs
         e o AddPaarmeter para adicionar os parametros que são pedidos nas procedures e para finalizae
         usa o ExecuteNonQuery para executar procedures que não retornam valores*/
        public void EditarCompraProduto(CompraProdutoDto compraProduto)
        {
            ExecuteProcedure(Procedures.CSSP_UpdCompraProduto);
            AddParameter("@IdProduto", compraProduto.Produto.IdProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);

            ExecuteNonQuery();
        }

        public void InserirCompraProduto(CompraProdutoDto compraProduto)
        {
            ExecuteProcedure(Procedures.CSSP_InsCompraProduto);
            AddParameter("@IdProduto", compraProduto.Produto.IdProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);
            ExecuteNonQuery();
        }

        public IEnumerable<CompraProdutoDto> ListarCompraProduto()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraProduto);
            var retorno = new List<CompraProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add( new CompraProdutoDto()
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        QtdeCompra = reader.ReadAsInt("QtdeProduto"),
                        Produto = new ProdutoDto()
                        {
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            NomeProduto = reader.ReadAsString("NomeProduto"),
                            PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                            Ativo = reader.ReadAsString("Ativo")
                        }
                    });
            return retorno;
        }

        public IEnumerable<CompraProdutoDto> ListarCompraProdutoIdVenda(int idVenda)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraProdutoIdVenda);
            AddParameter("@IdCompra", idVenda);
            var retorno = new List<CompraProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraProdutoDto()
                    {                        
                        QtdeCompra = reader.ReadAsInt("QtdeProduto"),
                        Produto = new ProdutoDto
                        {                            
                            NomeProduto = reader.ReadAsString("NomeProduto"),
                            PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),                            
                        }
                    });
            return retorno;
        }
    }
}
