using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;
using CandyShop.Repository.Database;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys


{
    public class ProdutoRepository : ConnectDB, IProdutoRepository
    {
        private enum Procedures
        {
            CSSP_InsProduto,
            CSSP_DesProduto,
            CSSP_UpdProduto,
            CSSP_SelProduto,
            CSSP_LisProduto,
            CSSP_LisProdutoInativo,
            CSSP_LisProdutoValorCres,
            CSSP_LisProdutoValorDesc,
            CSSP_LisProdutoAbaixoValor,
            CSSP_LisProdutoAcimaValor,
            CSSP_LisProdutoCategoria,
            CSSP_SelDadosProduto,
            CSSP_LisProdPorNome
        }

        public void InserirProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.CSSP_InsProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);
            AddParameter("@Ativo", "A");
            AddParameter("@Categoria", produto.Categoria);
            ExecuteNonQuery();
        }

        //não será mais usado temporariamente
        public void DesativarProduto(int idProduto) 
        {
            ExecuteProcedure(Procedures.CSSP_DesProduto);
            AddParameter("@IdProduto", idProduto);
            ExecuteNonQuery();
        }

        public void UpdateProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.CSSP_UpdProduto);
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
            ExecuteProcedure(Procedures.CSSP_SelProduto);
            AddParameter("@NomeProduto", nomeproduto);
            using (var retornobd = ExecuteReader())
                return retornobd.Read();
        }

        public IEnumerable<ProdutoDto> ListarProdutos()
        {
            ExecuteProcedure(Procedures.CSSP_LisProduto);
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
            ExecuteProcedure(Procedures.CSSP_SelDadosProduto);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoInativo);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoValorCres);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoValorDesc);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoAbaixoValor);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoAcimaValor);
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
            ExecuteProcedure(Procedures.CSSP_LisProdutoCategoria);            
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

        public IEnumerable<ProdutoDto> ProcurarProdutoPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisProdPorNome);
            AddParameter("@NomeProduto", nome);
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