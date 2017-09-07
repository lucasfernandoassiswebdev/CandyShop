using CandyShop.Core.Compra.Dto;
using System.Collections;

namespace CandyShop.Repository
{
    public class CompraRepository : ConnectDB
    {
        private enum Procedures
        {
            GCS_InsCompra,
            GCS_UpdCompra,
            GCS_LisCompra,
            GCS_DelCompra,
            GCS_LisCpfCompra

        }
        public void InserirCompra(CompraDto compra)
        {
            ExecuteProcedure(Procedures.GCS_InsCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameter("@DataCompra", compra.DataCompra);

            ExecuteNonQuery();
        }

        public void EditarCompra(CompraDto compra)
        {
            ExecuteProcedure(Procedures.GCS_UpdCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameter("@IdCompra", compra.IdCompra);
            AddParameter("@DataCompra", compra.DataCompra);

            ExecuteNonQuery();
        }

        public void DeletarCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.GCS_DelCompra);
            AddParameter("@IdCompra", idCompra);
            ExecuteNonQuery();
        }

        public IEnumerable ListarCompras()
        {
            ExecuteProcedure(Procedures.GCS_LisCompra);
            return ExecuteReader();
        }

        public IEnumerable ListarComprasPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_LisCpfCompra);
            AddParameter("@Cpf", cpf);
            return ExecuteReader();
        }
    }
}
