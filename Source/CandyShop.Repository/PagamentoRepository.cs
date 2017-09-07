using CandyShop.Core.Pagamento.Dto;
using System.Collections;

namespace CandyShop.Repository
{
    public class PagamentoRepository : ConnectDB
    {
        private enum Procedures
        {
            GCS_InsPagamento,
            GCS_UpdPagamento,
            GCS_DelPagamento,
            GCS_LisPagamento,
            GCS_LisCpfPagamento
        }
        public void Inserir(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.GCS_InsPagamento);
            AddParameter("IdPagamento", pagamento.IdPagamento);
            AddParameter("UsuarioPagamento", pagamento.Usuario);
            AddParameter("DataPagamento", pagamento.DataPagamento);
            AddParameter("ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

        public void Editar(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.GCS_UpdPagamento);
            AddParameter("IdPagamento", pagamento.IdPagamento);
            AddParameter("DataPagamento", pagamento.DataPagamento);
            AddParameter("ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

        public void Excluir(int idPagamento)
        {
            ExecuteProcedure(Procedures.GCS_DelPagamento);
            AddParameter("IdPagamento", idPagamento);
            ExecuteNonQuery();
        }

        public IEnumerable ListarPagamentos()
        {
            ExecuteProcedure(Procedures.GCS_LisPagamento);
            return ExecuteReader();
        }

        public IEnumerable SelecionarPagamentosPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_LisCpfPagamento);
            AddParameter("Cpf", cpf);
            using (var retorno = ExecuteReader())
                return retorno;
        }

    }
}
