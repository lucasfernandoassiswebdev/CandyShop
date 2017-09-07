using CandyShop.Core;
using CandyShop.Core.Compra.Dto;
using CandyShop.Core.Usuario.Dto;
using Concessionaria.Repositorio;
using System.Collections;

namespace CandyShop.Repository
{
    public class CompraRepository : ConnectDB, ICompraRepository
    {
        private enum Procedures
        {
            GCS_InsCompra,
            GCS_UpdCompra,
            GCS_LisCompra,
            GCS_DelCompra,
            GCS_LisCpfCompra,
            GCS_SelCompra,


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

        public IEnumerable ListarCompra()
        {
            ExecuteProcedure(Procedures.GCS_LisCompra);
            return ExecuteReader();
        }

        public IEnumerable ListarCompraPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_LisCpfCompra);
            AddParameter("@Cpf", cpf);
            return ExecuteReader();
        }

        public bool SelecionarCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.GCS_SelCompra);
            AddParameter("@IdCompra", idCompra);
            using (var retorno = ExecuteReader())
                return retorno.Read();
        }

        public CompraDto SelecionarDadosCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.GCS_SelCompra);
            AddParameter("@IdCompra", idCompra);
            CompraDto retorno = new CompraDto();
            using (var reader = ExecuteReader())
            {
                if (reader.Read())
                    retorno = new CompraDto
                    {
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        Usuario = new UsuarioDto()
                        {
                            Cpf = reader.ReadAsString("UsuarioCompra")
                        }
                    };
            }
            return retorno;
        }
    }
}
