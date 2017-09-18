
using CandyShop.Core.Services.Produto.Dto;
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
            GCS_LisProduto,
            GCS_LisProdutoInativo,
            GCS_LisProdutoValorCres,
            GCS_LisProdutoValorDesc,
            GCS_LisProdutoAbaixoValor,
            GCS_LisProdutoAcimaValor,
            GCS_LisProdutoCategoria
        }

        public void InserirProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.GCS_InsProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);
            AddParameter("@Ativo", "A");
            AddParameter("@Categoria", produto.Categoria);
            ExecuteNonQuery();
        }

        //não será mais usado temporariamente
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
            AddParameter("@Ativo", produto.Ativo);
            AddParameter("@Categoria", produto.Categoria);
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
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
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
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    };
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosInativos()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoInativo);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosValorCrescente()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoValorCres);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosValorDecrescente()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoValorDesc);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosAbaixoValor()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoAbaixoValor);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosAcimaValor()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoAcimaValor);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }

        public IEnumerable<ProdutoDto> ListarProdutosPorCategoria()
        {
            ExecuteProcedure(Procedures.GCS_LisProdutoCategoria);
            var retorno = new List<ProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new ProdutoDto
                    {
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        NomeProduto = reader.ReadAsString("NomeProduto"),
                        PrecoProduto = reader.ReadAsDecimal("PrecoProduto"),
                        QtdeProduto = reader.ReadAsInt("QtdeProduto"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Categoria = reader.ReadAsString("Categoria")
                    });
            return retorno;
        }
    }
}