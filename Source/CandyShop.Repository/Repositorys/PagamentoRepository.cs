using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Pagamento.Dto;
using CandyShop.Core.Services.Usuario.Dto;
using CandyShop.Repository.Database;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class PagamentoRepository : ConnectDB, IPagamentoRepository
    {
        private enum Procedures
        {
            CSSP_InsPagamento,
            CSSP_UpdPagamento,
            CSSP_DelPagamento,
            CSSP_LisPagamento,
            CSSP_LisCpfPagamento,
            CSSP_SelPagamento
        }

        public void InserirPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_InsPagamento);
            AddParameter("@Cpf", pagamento.Usuario.Cpf);
            AddParameter("@DataPagamento", pagamento.DataPagamento);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);
            ExecuteNonQuery();
        }

        public void EditarPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_UpdPagamento);
            AddParameter("@IdPagamento", pagamento.IdPagamento);
            AddParameter("@DataPagamento", pagamento.DataPagamento);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

        public void DeletarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_DelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            ExecuteNonQuery();
        }

        public IEnumerable<PagamentoDto> ListarPagamentos()
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            var retorno = new List<PagamentoDto>();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    do
                    {
                        retorno.Add( new PagamentoDto
                        {
                            DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                            IdPagamento = reader.ReadAsInt("IdPagamento"),
                            ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                            Usuario = new UsuarioDto()
                            {
                                Cpf = reader.ReadAsString("Cpf")
                            }
                        });
                    } while (reader.Read());
            return retorno;
        }

        public IEnumerable<PagamentoDto> ListarPagamentosPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisCpfPagamento);
            AddParameter("@Cpf", cpf);
            var retorno = new List<PagamentoDto>();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    do
                    {
                        retorno.Add(new PagamentoDto
                        {
                            DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                            IdPagamento = reader.ReadAsInt("IdPagamento"),
                            ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                            NomeUsuario = reader.ReadAsString("NomeUsuario"),
                            Usuario = new UsuarioDto
                            {
                                Cpf = reader.ReadAsString("Cpf")
                            }
                        });
                    } while (reader.Read());
            return retorno;
        }

        public bool SelecionarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            using (var retorno = ExecuteReader())
                return retorno.Read();
        }

        public PagamentoDto SelecionarDadosPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            PagamentoDto retorno = new PagamentoDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new PagamentoDto
                    {
                        DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                        IdPagamento = reader.ReadAsInt("IdPagamento"),
                        ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                        Usuario = new UsuarioDto
                        {
                            Cpf = reader.ReadAsString("Cpf")
                        }
                    };
            return retorno;
        }
    }
}
