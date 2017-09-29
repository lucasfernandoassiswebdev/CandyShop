using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Produto.Dto;
using CandyShop.Repository.Database;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace CandyShop.Repository.Repositorys
{
    public class CompraProdutoRepository : Execucao, ICompraProdutoRepository
    {
        /* A classe de execução exige um parâmetro no seu construtor, no caso
           uma instância de conexão, ao herdar dess classe, a classe que está herdando
           deve passar em seu construtor o construtor que é exigido pela classe que está
           sendo herdada, assim como feito abaixo */
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

        /* O método ExecuteProcedure é o metodo encapsulado para montar os
           comandos que serão executados no banco, o método encapsulado 
           AddPaarmeter adiciona os parametros que são pedidos nas 
           procedures e para finalizar o método ExecuteNonQuery executa procedures 
           que não retornam valores */
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

        /*
         Nesse caso a procedure retornará uma lista de ubjetos, por
         isso aqui é osado o método ExecuteReader(), e enquanto o reader estiver
         lendo as linhas que foram retornadas pela procedure, são criados novos objetos
         e adicionados numa lista de objetos, quando o reader terminar de ser executado
         a lista de objetos é retornada */
        public IEnumerable<CompraProdutoDto> ListarCompraProduto()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraProduto);
            var retorno = new List<CompraProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraProdutoDto()
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

            //verificando se a lista de objetos não veio nula e a retornando caso sim
            return retorno.Any() ? retorno : null;
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
            return retorno.Any() ? retorno : null;
        }
    }
}
