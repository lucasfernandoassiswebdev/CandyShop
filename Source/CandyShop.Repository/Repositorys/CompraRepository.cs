using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Usuario.Dto;
using CandyShop.Repository.Database;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class CompraRepository : ConnectDB, ICompraRepository
    {
        private enum Procedures
        {
            CSSP_InsCompra,
            CSSP_UpdCompra,
            CSSP_LisCompra,
            CSSP_DelCompra,
            CSSP_LisCpfCompra,
            CSSP_SelCompra,
            CSSP_InsCompraProduto,
            CSSP_UpdCompraProduto,
            CSSP_DelCompraProduto,
            CSSP_LisCompraNomeUsuario
        }

        public void InserirCompra(CompraDto compra)
        {
            ExecuteProcedure(Procedures.CSSP_InsCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
            AddParameter("@DataCompra", compra.DataCompra);

            ExecuteNonQuery();
        }

        public void InserirItens(CompraProdutoDto item)
        {
            ExecuteProcedure(Procedures.CSSP_InsCompraProduto);
            AddParameter("@IdProduto", item.Produto.IdProduto);
            AddParameter("@QtdeProduto", item.QtdeCompra);
            AddParameter("@IdCompra", item.IdCompra);
            ExecuteNonQuery();

        }

        public void EditarCompra(CompraDto compra)
        {
            ExecuteProcedure(Procedures.CSSP_UpdCompra);
            AddParameter("@UsuarioCompra", compra.Usuario.Cpf);
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

        public IEnumerable<CompraDto> ListarCompra()
        {
            ExecuteProcedure(Procedures.CSSP_LisCompra);
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
            ExecuteProcedure(Procedures.CSSP_LisCpfCompra);
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
            ExecuteProcedure(Procedures.CSSP_SelCompra);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
        }

        public CompraDto SelecionarDadosCompra(int idCompra)
        {
            ExecuteProcedure(Procedures.CSSP_SelCompra);
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

        public IEnumerable<CompraDto> ListarCompraPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisCompraNomeUsuario);
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
