using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.Usuario;
using CandyShop.Repository.Database;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;
using System.Data;

namespace CandyShop.Repository.Repositorys
{
    public class CompraRepository : Execucao, ICompraRepository
    {
        public CompraRepository(Conexao conexao) : base(conexao)
        {
            
        }

        private enum Procedures
        {
            CSSP_InsCompra,
            CSSP_UpdCompra,
            CSSP_LisCompra,
            CSSP_DelCompra,
            CSSP_SelCompra,
            CSSP_UpdCompraProduto,
            CSSP_DelCompraProduto,
            CSSP_LisCompraNomeUsuario,
            CSSP_LisCompraSemana,
            CSSP_LisCompraDia,
            CSSP_LisCpfCompra,
            CSSP_SelDadosCompra
        }

        public int InserirCompra(Compra compra, out int sequencial)
        {
            sequencial = 0;
            ExecuteProcedure(Procedures.CSSP_InsCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameterOutput("@sequencial", sequencial, DbType.Int32);
            var retorno = ExecuteNonQueryWithReturn();
            sequencial = int.Parse(GetParameterOutput("@sequencial"));
            return retorno;
        }

        public void EditarCompra(Compra compra)
        {
            ExecuteProcedure(Procedures.CSSP_UpdCompra);
            AddParameter("@UsuarioCompra", compra.Usuario);
            AddParameter("@IdCompra", compra.IdCompra);
            AddParameter("@DataCompra", compra.DataCompra);

            ExecuteNonQuery();
        }

        public void DeletarCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.CSSP_DelCompra);
            AddParameter("@IdCompra", idCompra);
            ExecuteNonQuery();
        }

        public int SelecionarCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.CSSP_SelCompra);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
        }

        public Compra SelecionarDadosCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.CSSP_SelDadosCompra);
            AddParameter("@IdCompra", idCompra);
            var retorno = new Compra();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new Compra
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        ValorCompra = reader.ReadAsDecimal("ValorCompra"),
                        Usuario = new Usuario()
                        {
                            NomeUsuario = reader.ReadAsString("NomeUsuario")
                        }
                    };

            return retorno;

        }

        public void EditaItens(CompraProduto compraProduto)
        {
            ExecuteProcedure(Procedures.CSSP_UpdCompraProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@IdProduto", compraProduto.Produto.IdProduto);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);
            ExecuteNonQuery();
        }

        public void DeletaItens(int idcompra, int idproduto)
        {
            ExecuteProcedure(Procedures.CSSP_DelCompraProduto);
            AddParameter("@IdCompra", idcompra);
            AddParameter("@IdProduto", idproduto);
            ExecuteNonQuery();
        }

        public IEnumerable<Compra> ListarCompra()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompra);
            return Listar();
        }

        public IEnumerable<Compra> ListarCompraMes(int mes)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompra);
            AddParameter("@mes", mes);
            return Listar();
        }

        public IEnumerable<Compra> ListarCompraPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisCpfCompra);
            AddParameter("@Cpf", cpf);
            return Listar();
        }

        public IEnumerable<Compra> ListarCompraPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraNomeUsuario);
            AddParameter("@Nome", nome);
            return Listar();
        }

        public IEnumerable<Compra> ListarCompraSemana()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraSemana);
            return Listar();
        }

        public IEnumerable<Compra> ListarCompraDia()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraDia);
            return Listar();
        }

        private IEnumerable<Compra> Listar()
        {
            var retorno = new List<Compra>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new Compra()
                    {

                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        ValorCompra = reader.ReadAsDecimalNull("ValorCompra"),
                        Usuario = new Usuario()
                        {
                            NomeUsuario = reader.ReadAsString("NomeUsuario"),
                            Cpf = reader.ReadAsString("UsuarioCompra")
                        }
                    });
            return retorno;
        }
    }
}
