using CandyShop.Core.Pagamento.Dto;

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
        private void Inserir(Pagamento pagamento)
        {
            ExecuteProcedure(Procedures.GCS_InsPagamento);
            AddParameter("IdPagamento", pagamento.IdPagamento);
            AddParameter("UsuarioPagamento", pagamento.Usuario);
            AddParameter("DataPagamento", pagamento.DataPagamento);
            AddParameter("ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }

    }
}
