using CandyShop.Core.CompraProduto.Dto;
using CandyShop.Core.Services.CompraProduto;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class CompraProdutoRepository : ConnectDB, ICompraProdutoRepository
    {
        private enum Procedures
        {
            GCS_LisCompraProduto,
            GCS_LisCompraProdutoIdVenda,
            GCS_InsCompraProduto,
            GCS_UpdCompraProduto
        }

        public void EditarCompraProduto(CompraProdutoDto compraProduto)
        {
            ExecuteProcedure(Procedures.GCS_UpdCompraProduto);
            AddParameter("@IdProduto", compraProduto.IdProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);

            ExecuteNonQuery();
        }

        public void InserirCompraProduto(CompraProdutoDto compraProduto)
        {
            ExecuteProcedure(Procedures.GCS_InsCompraProduto);
            AddParameter("@IdProduto", compraProduto.IdProduto);
            AddParameter("@IdCompra", compraProduto.IdCompra);
            AddParameter("@QtdeProduto", compraProduto.QtdeCompra);

            ExecuteNonQuery();
        }

        public IEnumerable<CompraProdutoDto> ListarCompraProduto()
        {
            ExecuteProcedure(Procedures.GCS_LisCompraProduto);
            var retorno = new List<CompraProdutoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new CompraProdutoDto()
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        QtdeCompra = reader.ReadAsInt("QtdeProduto")
                    });
            return retorno;
        }

        public CompraProdutoDto ListarCompraProdutoIdVenda(int idVenda)
        {
            ExecuteProcedure(Procedures.GCS_LisCompraProdutoIdVenda);
           
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new CompraProdutoDto()
                    {
                        IdCompra = reader.ReadAsInt("IdCompra"),
                        IdProduto = reader.ReadAsInt("IdProduto"),
                        QtdeCompra = reader.ReadAsInt("QtdeProduto")
                    };
            return null;
        }
    }
}
