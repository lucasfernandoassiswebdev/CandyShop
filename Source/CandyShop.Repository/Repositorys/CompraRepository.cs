using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Usuario.Dto;
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

        public int InserirCompra(CompraDto compra, out int sequencial)
        {
            sequencial = 0;
            ExecuteProcedure(Procedures.CSSP_InsCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameterOutput("@sequencial", sequencial, DbType.Int32);
            var retorno = ExecuteNonQueryWithReturn();
            sequencial = int.Parse(GetParameterOutput("@sequencial"));
            return retorno;
        }

        public void EditarCompra(CompraDto compra)
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

        public CompraDto SelecionarDadosCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.CSSP_SelDadosCompra);
            AddParameter("@IdCompra", idCompra);
            var retorno = new CompraDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new CompraDto
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        ValorCompra = reader.ReadAsDecimal("ValorCompra"),
                        Usuario = new UsuarioDto()
                        {
                            NomeUsuario = reader.ReadAsString("NomeUsuario")
                        }
                    };

            return retorno;

        }

        public void EditaItens(CompraProdutoDto compraProduto)
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

        public IEnumerable<CompraDto> ListarCompra()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompra);
            return Listar();
        }

        public IEnumerable<CompraDto> ListarCompraMes(int mes)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompra);
            AddParameter("@mes", mes);
            return Listar();
        }

        public IEnumerable<CompraDto> ListarCompraPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisCpfCompra);
            AddParameter("@Cpf", cpf);
            return Listar();
        }

        public IEnumerable<CompraDto> ListarCompraPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraNomeUsuario);
            AddParameter("@Nome", nome);
            return Listar();
        }

        public IEnumerable<CompraDto> ListarCompraSemana()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraSemana);
            return Listar();
        }

        public IEnumerable<CompraDto> ListarCompraDia()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraDia);
            return Listar();
        }

        private IEnumerable<CompraDto> Listar()
        {
            var retorno = new List<CompraDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraDto()
                    {

                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        ValorCompra = reader.ReadAsDecimalNull("ValorCompra"),
                        Usuario = new UsuarioDto()
                        {
                            NomeUsuario = reader.ReadAsString("NomeUsuario"),
                            Cpf = reader.ReadAsString("UsuarioCompra")
                        }
                    });
            return retorno;
        }
    }
}
