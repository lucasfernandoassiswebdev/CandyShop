using CandyShop.Core.Services.Produto;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;
using System.Data;

namespace CandyShop.Repository.Repositorys
{
    public class ProdutoRepository : Execucao, IProdutoRepository
    {
        public ProdutoRepository(Conexao conexao) : base(conexao)
        {

        }
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
            CSSP_LisProdPorNome,
            CSSP_LisProdutoAeI

        }

        public int InserirProduto(Produto produto, out int sequencial)
        {
            sequencial = 0;
            ExecuteProcedure(Procedures.CSSP_InsProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);
            AddParameter("@Ativo", "A");
            AddParameter("@Categoria", produto.Categoria);
            AddParameterOutput("@sequencial",sequencial, DbType.Int32);
            var retorno = ExecuteNonQueryWithReturn();
            sequencial = int.Parse(GetParameterOutput("@sequencial"));
            return retorno;
        }
        public void DesativarProduto(int idProduto)
        {
            ExecuteProcedure(Procedures.CSSP_DesProduto);
            AddParameter("@IdProduto", idProduto);
            ExecuteNonQuery();
        }
        public void UpdateProduto(Produto produto)
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
        public Produto SelecionarDadosProduto(int idProduto)
        {
            ExecuteProcedure(Procedures.CSSP_SelDadosProduto);
            AddParameter("@IdProduto", idProduto);

            var retorno = new Produto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new Produto
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
        public IEnumerable<Produto> ListarProdutos()
        {
            ExecuteProcedure(Procedures.CSSP_LisProduto);
            return Listar();
        }        
        public IEnumerable<Produto> ListarProdutosInativos()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoInativo);
            return Listar();
        }
        public IEnumerable<Produto> ListarProdutosValorCrescente()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoValorCres);
            return Listar();
        }
        public IEnumerable<Produto> ListarProdutosValorDecrescente()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoValorDesc);
            return Listar();
        }
        public IEnumerable<Produto> ListarProdutosAbaixoValor()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoAbaixoValor);
            return Listar();
        }
        public IEnumerable<Produto> ListarProdutosAcimaValor()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoAcimaValor);
            return Listar();
        }
        public IEnumerable<Produto> ListarProdutosPorCategoria(string categoria)
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoCategoria);
            AddParameter("@Categoria", categoria);
            return Listar();
        }
        public IEnumerable<Produto> ProcurarProdutoPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisProdPorNome);
            AddParameter("@NomeProduto", nome);
            
            return Listar();
        }

        private IEnumerable<Produto> Listar()
        {
            var retorno = new List<Produto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new Produto
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

        public IEnumerable<Produto> ListarProdutoAeI()
        {
            ExecuteProcedure(Procedures.CSSP_LisProdutoAeI);
            return Listar();
        }
    }
}