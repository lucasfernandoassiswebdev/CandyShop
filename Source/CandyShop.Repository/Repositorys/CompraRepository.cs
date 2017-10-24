using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Usuario;
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
            CSSP_LisCompra,
            CSSP_SelCompra,
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
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Compra
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        ValorCompra = reader.ReadAsDecimal("ValorCompra"),
                        Usuario = new Usuario()
                        {
                            NomeUsuario = reader.ReadAsString("NomeUsuario"),
                            Classificacao = reader.ReadAsString("Classificacao")
                        }
                    };

            return null;
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
