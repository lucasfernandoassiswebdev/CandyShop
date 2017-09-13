
using CandyShop.Core.Services.Produto.Dto;
using Concessionaria.Repositorio;
using System.Collections.Generic;

namespace CandyShop.Repository


{
    public class ProdutoRepository : ConnectDB, IProdutoRepository
    {
        private enum Procedures
        {
            GCS_InsProduto,
            GCS_DelProduto,
            GCS_UpdProduto,
            GCS_SelProduto,
            GCS_LisProduto
        }

        public void InserirProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.GCS_InsProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);

            ExecuteNonQuery();
        }

        public void DeletarProduto(int idproduto)
        {
            ExecuteProcedure(Procedures.GCS_DelProduto);
            AddParameter("@IdProduto", idproduto);
            ExecuteNonQuery();
        }

        public void UpdateProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.GCS_UpdProduto);
            AddParameter("@IdProduto", produto.IdProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);
            ExecuteNonQuery();
        }

        public bool SelecionarProduto(string nomeproduto)
        {
            ExecuteProcedure(Procedures.GCS_SelProduto);
            AddParameter("@NomeProduto", nomeproduto);
            using (var retornobd = ExecuteReader())
                return retornobd.Read();
        }

        public IEnumerable<ProdutoDto> ListarProdutos()
        {
            ExecuteProcedure(Procedures.GCS_LisProduto);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    do
                    {
                        retorno.Add(new ProdutoDto
                        {
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            NomeProduto = reader.ReadAsString("NomeProduto"),
                            PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                            QtdeProduto = reader.ReadAsInt("QtdeProduto")
                        });
                    } while (reader.Read());

            return retorno;
        }

        public ProdutoDto SelecionarDadosProduto(int idProduto)
        {
            ExecuteProcedure(Procedures.GCS_SelProduto);
            AddParameter("@IdProduto", idProduto);

            var retorno = new ProdutoDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto")
                    };
            return retorno;
        }   
    }
}