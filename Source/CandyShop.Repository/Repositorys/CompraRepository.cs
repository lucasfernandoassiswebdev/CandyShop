using CandyShop.Core;
using CandyShop.Core.Compra.Dto;
using CandyShop.Core.CompraProduto.Dto;
using CandyShop.Core.Usuario.Dto;
using System.Collections.Generic;

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
            GCS_InsCompraProduto,
            GCS_UpdCompraProduto,
            GCS_DelCompraProduto,
            GCS_LisCompraNomeUsuario
        }

        public void InserirCompra(CompraDto compra)
        {
            ExecuteProcedure(Procedures.GCS_InsCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameter("@DataCompra", compra.DataCompra);

            ExecuteNonQuery();

        }

        public void InserirItens(CompraProdutoDto item)
        {
            ExecuteProcedure(Procedures.GCS_InsCompraProduto);
            AddParameter("@IdProduto", item.IdProduto);
            AddParameter("@QtdeProduto", item.QtdeCompra);
            AddParameter("@IdCompra", item.IdCompra);
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

        public IEnumerable<CompraDto> ListarCompra()
        {
            ExecuteProcedure(Procedures.GCS_LisCompra);
            var retorno = new List<CompraDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraDto()
                    {

                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        Usuario = new UsuarioDto()
                        {
                            NomeUsuario = reader.ReadAsString("nomeUsuario"),
                            Cpf = reader.ReadAsString("UsuarioCompra")
                        }
                    });
            return retorno;
        }

        public IEnumerable<CompraDto> ListarCompraPorCpf(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_LisCpfCompra);
            AddParameter("@Cpf", cpf);
            var retorno = new List<CompraDto>();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    do
                    {
                        retorno.Add(new CompraDto()
                        {
                            DataCompra = reader.ReadAsDateTime("DataCompra"),
                            IdCompra = reader.ReadAsInt("IdCompra"),
                            Usuario = new UsuarioDto()
                            {
                                Cpf = reader.ReadAsString("UsuarioCompra")
                            }
                        });
                    } while (reader.Read());

            return retorno;
        }

        public int SelecionarCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.GCS_SelCompra);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
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

        public void EditaItens(CompraProdutoDto compraProduto)
        {
            ExecuteProcedure(Procedures.GCS_UpdCompraProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@IdProduto", compraProduto.IdProduto);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);
            ExecuteNonQuery();
        }

        public void DeletaItens(int idcompra, int idproduto)
        {
            ExecuteProcedure(Procedures.GCS_DelCompraProduto);
            AddParameter("@IdCompra", idcompra);
            AddParameter("@IdProduto", idproduto);
            ExecuteNonQuery();
        }

        public IEnumerable<CompraDto> ListarCompraPorNome(string nome)
        {
            ExecuteProcedure(Procedures.GCS_LisCompraNomeUsuario);
            AddParameter("@Nome", nome);
            var retorno = new List<CompraDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraDto()
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        DataCompra = reader.ReadAsDateTime("DataCompra"),
                        Usuario = new UsuarioDto()
                        {
                            NomeUsuario = reader.ReadAsString("nomeUsuario"),
                            Cpf = reader.ReadAsString("UsuarioCompra")
                        }
                    });
            return retorno;
        }
    }
}
