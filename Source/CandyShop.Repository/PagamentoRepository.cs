using CandyShop.Core;
using CandyShop.Core.Pagamento.Dto;
using CandyShop.Core.Usuario.Dto;
using Concessionaria.Repositorio;
using System.Collections;

namespace CandyShop.Repository
{
    public class PagamentoRepository : ConnectDB, IPagamentoRepository
    {
        private enum Procedures
        {
            GCS_InsPagamento,
            GCS_UpdPagamento,
            GCS_DelPagamento,
            GCS_LisPagamento,
            GCS_LisCpfPagamento,
            GCS_SelPagamento
        }

        public void InserirPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.GCS_InsPagamento);
            AddParameter("@IdPagamento", pagamento.IdPagamento);
            AddParameter("@UsuarioPagamento", pagamento.Usuario.Cpf);
            AddParameter("@DataPagamento", pagamento.DataPagamento);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

        public void EditarPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.GCS_UpdPagamento);
            AddParameter("@IdPagamento", pagamento.IdPagamento);
            AddParameter("@DataPagamento", pagamento.DataPagamento);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

        public void DeletarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.GCS_DelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            ExecuteNonQuery();
        }

        public IEnumerable ListarPagamentos()
        {
            ExecuteProcedure(Procedures.GCS_LisPagamento);
            return ExecuteReader();
        }

        public IEnumerable ListarPagamentosPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_LisCpfPagamento);
            AddParameter("@Cpf", cpf);
            using (var retorno = ExecuteReader())
                return retorno;
        }

        public bool SelecionarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.GCS_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            using (var retorno = ExecuteReader())
                return retorno.Read();
        }

        public PagamentoDto SelecionarDadosPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.GCS_SelPagamento);
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
